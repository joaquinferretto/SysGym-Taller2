const fs = require('fs');
const path = require('path');
const files = dir => fs.readdirSync(dir, {withFileTypes:true}).flatMap(e => e.isDirectory() ? files(path.join(dir,e.name)) : [path.join(dir,e.name)]);
function mask(s) { return s.replace(/@"(?:""|[^"])*"|"(?:\\.|[^"\\])*"|'(?:\\.|[^'\\])*'|\/\/[^\r\n]*|\/\*[\s\S]*?\*\//g, x => x.replace(/[^\r\n]/g,' ')); }
function end(s, start, open='{',close='}') { const m=mask(s); let n=0; for(let i=start;i<m.length;i++){if(m[i]===open)n++;if(m[i]===close&&!--n)return i;} throw Error('Unbalanced'); }
function edits(s, xs){for(const x of xs.sort((a,b)=>b.start-a.start))s=s.slice(0,x.start)+x.text+s.slice(x.end);return s;}
const eventType = e => ({KeyPress:'System.Windows.Forms.KeyPressEventHandler',Validating:'System.ComponentModel.CancelEventHandler',Format:'System.Windows.Forms.ListControlConvertEventHandler',FormClosed:'System.Windows.Forms.FormClosedEventHandler'}[e]||'System.EventHandler');
const argType = e => ({KeyPress:'KeyPressEventArgs',Validating:'CancelEventArgs',Format:'ListControlConvertEventArgs',FormClosed:'FormClosedEventArgs'}[e]||'EventArgs');
function events(){
 const report=[];
 for(const df of files('capaVisual').filter(f=>f.endsWith('.Designer.cs'))){
  const cf=df.replace('.Designer.cs','.cs'), cls=path.basename(cf,'.cs');
  let cs=fs.readFileSync(cf,'utf8'), ds=fs.readFileSync(df,'utf8');
  const added=[], bindings=[], renames=new Map();
  function bind(control,event,rhs){
   const name=(control==='this'?cls:control)+'_'+event;
   if(rhs.startsWith('delegate')){
    const p=rhs.indexOf('{'); const body=rhs.slice(p+1,end(rhs,p));
    added.push(`        private void ${name}(object sender, ${argType(event)} e)\n        {\n            ${body.trim()}\n        }\n`);
   } else {
    const old=rhs.replace(/^new [\w.]+\((?:this\.)?(\w+)\)$/,'$1').replace(/^this\./,'').trim();
    if(old!==name)renames.set(old,name);
   }
   bindings.push({control,event,name});
  }
  function extract(s, onlyConstructor){
   const xs=[], m=mask(s); const re=/(?:(?:this\.)?(\w+)\.)?(Click|TextChanged|SelectedIndexChanged|SelectionChanged|ValueChanged|KeyPress|Validating|Format|Load)\s*\+=\s*/g;
   let x; while(x=re.exec(m)){
    const start=x.index, p=re.lastIndex; let stop;
    if(s.slice(p).startsWith('delegate')){const b=m.indexOf('{',p);stop=m.indexOf(';',end(s,b))+1;}else stop=m.indexOf(';',p)+1;
    bind(x[1]||'this',x[2],s.slice(p,stop-1).trim());xs.push({start,end:stop,text:''});re.lastIndex=stop;
   }
   return edits(s,xs);
  }
  cs=cs.replace(/FormularioVisualHelper\.AlCargarEnEjecucion\(this, delegate \{ ([^\n]*?) \}\);/g,(_,body)=>{
   bind('this','Load',`delegate { if (FormularioVisualHelper.EnModoDisenio(this)) return; ${body} }`);return '';
  });
  cs=extract(cs); ds=extract(ds);
  if(['GestionUsuariosForm','GestionPlanesForm'].includes(cls)){
   for(const [c,h] of Object.entries({nuevo:cls==='GestionUsuariosForm'?'NuevoUsuario':'NuevoPlan',guardar:'GuardarNuevo',actualizar:'Actualizar',darDeBaja:'DarDeBaja',reactivar:'Reactivar'})){bind(c,'Click',h);report.push(`${cls}: faltaba ${c}.Click`);}
  }
  for(const [old,n]of renames){
   cs=cs.replace(new RegExp('\\b'+old+'(?=\\s*\\()','g'),n);
   // Los handlers serializados son siempre de instancia.
   cs=cs.replace(new RegExp('private static void '+n+'\\('),'private void '+n+'(');
  }
  let insertion=cs.lastIndexOf('\n    }');cs=cs.slice(0,insertion)+'\n'+added.join('\n')+cs.slice(insertion);
  for(const [old,n]of renames)cs=cs.replace(new RegExp('\\b'+old+'(?=\\s*\\()','g'),n);
  const init=ds.indexOf('{',ds.indexOf('private void InitializeComponent()'));const finish=end(ds,init);
  const wiring=bindings.map(b=>`            this.${b.control==='this'?'':b.control+'.'}${b.event} += new ${eventType(b.event)}(this.${b.name});`).join('\n');
  ds=ds.slice(0,finish)+'\n'+wiring+'\n        '+ds.slice(finish);
  fs.writeFileSync(cf,cs);fs.writeFileSync(df,ds);
  report.push(`${cls}: ${bindings.map(b=>b.control+'.'+b.event).join(', ')}`);
 }
 console.log(report.join('\n'));
}
function readonly(){
 for(const f of files('capaLogica').filter(f=>f.endsWith('.cs'))){
  let s=fs.readFileSync(f,'utf8'),xs=[]; const re=/\bpublic\s+(?:static\s+)?[\w<>?]+\s+((?:Listar|Obtener|Calcular|Autenticar)\w*)\([^)]*\)\s*\{/g;let x;
  while(x=re.exec(mask(s))){const b=re.lastIndex-1,z=end(s,b),body=s.slice(b,z+1);if(!body.includes('new GymUnidadDeTrabajo()'))continue;
   let t=body.replace(/\b(context|datos)\.(\w+)\.Consultar\(/g,'$1.$2.ConsultarSoloLectura(')
    .replace(/\b(context|datos)\.(\w+)(\s*)\.(Where|OrderBy|OrderByDescending|SingleOrDefault|Any|Select)\(/g,'$1.$2.ConsultarSoloLectura()$3.$4(');
   if(x[1]==='ObtenerPorId' && f.endsWith('EjercicioLogica.cs'))t=t.replace('datos.Ejercicios.Buscar(idEjercicio)','datos.Ejercicios.ConsultarSoloLectura().SingleOrDefault(e => e.IdEjercicio == idEjercicio)');
   xs.push({start:b,end:z+1,text:t});re.lastIndex=z+1;
  }
  s=edits(s,xs).replace(/\b(context|datos)\.(\w+)\.Find\(/g,'$1.$2.Buscar(').replace(/\b(context|datos)\.(\w+)\.Add\(/g,'$1.$2.Agregar(');
  fs.writeFileSync(f,s);
 }
}
if(process.argv[2]==='events')events();
if(process.argv[2]==='readonly')readonly();
if(process.argv[2]==='recover-visual'){
 const cp=require('child_process');
 for(const f of [...files('capaVisual').filter(f=>f.endsWith('.cs')),'exxen2.0.csproj']){
  fs.writeFileSync(f,cp.execFileSync('git',['show','HEAD:'+f.replaceAll('\\','/')],{maxBuffer:2000000}));
 }
}
module.exports={files,mask,end,edits};
if(process.argv[2]==='load-errors'){
 for(const f of files('capaVisual').filter(f=>f.endsWith('.cs')&&!f.endsWith('.Designer.cs'))){
  let s=fs.readFileSync(f,'utf8'),xs=[]; if(!s.includes('lblEstado'))continue;
  const re=/private void \w+_Load\(object sender, EventArgs e\)\s*\{/g;let x;
  while(x=re.exec(s)){let b=re.lastIndex-1,z=end(s,b),body=s.slice(b+1,z);if(body.includes('try'))continue;
   body=body.replace('if (FormularioVisualHelper.EnModoDisenio(this)) return;','');
   xs.push({start:b+1,end:z,text:'\n            if (FormularioVisualHelper.EnModoDisenio(this)) return;\n            try\n            {\n'+body+'\n            }\n            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }\n        '});
  }
  fs.writeFileSync(f,edits(s,xs));
 }
}
if(process.argv[2]==='runtime-events'){
 for(const df of files('capaVisual').filter(f=>f.endsWith('.Designer.cs'))){
  const cf=df.replace('.Designer.cs','.cs'),cls=path.basename(cf,'.cs');let cs=fs.readFileSync(cf,'utf8'),ds=fs.readFileSync(df,'utf8'),methods=[],wires=[];
  cs=cs.replace(/FormularioVisualHelper\.ConfigurarEntradaDecimal\((\w+)\);/g,(_,c)=>{
   methods.push(`        private void ${c}_KeyPress(object sender, KeyPressEventArgs e)\n        {\n            FormularioVisualHelper.ValidarEntradaDecimal(${c}, e);\n        }`);
   wires.push(`this.${c}.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.${c}_KeyPress);`);return '';
  });
  if(/^Dashboard(Administrador|Entrenador|Recepcionista)$/.test(cls)){
   let body=[]; cs=cs.replace(/^\s*(lblUsuarioRol\.Text = [^\r\n]+|navegacion\.EstablecerContenidoInicio\([^\r\n]+)\r?$/gm,(_,s)=>{body.push(s);return '';});
   methods.push(`        private void ${cls}_Load(object sender, EventArgs e)\n        {\n            if (FormularioVisualHelper.EnModoDisenio(this)) return;\n            ${body.join('\n            ')}\n        }`);
   wires.push(`this.Load += new System.EventHandler(this.${cls}_Load);`);
  }
  const idx=cs.lastIndexOf('\n    }');cs=cs.slice(0,idx)+'\n'+methods.join('\n\n')+cs.slice(idx);
  const b=ds.indexOf('{',ds.indexOf('private void InitializeComponent()')),z=end(ds,b);
  ds=ds.slice(0,z)+wires.map(x=>'            '+x+'\n').join('')+'        '+ds.slice(z);
  cs=cs.replace(/^[ \t]+$/gm,'').replace(/\n{3,}/g,'\n\n');
  fs.writeFileSync(cf,cs);fs.writeFileSync(df,ds);
 }
}
if(process.argv[2]==='repair-calls'){
 const names={registrar_Click:'Registrar',darDeBaja_Click:'DarDeBaja',reactivar_Click:'Reactivar',agregarEjercicio_Click:'AgregarEjercicio',asignar_Click:'Asignar',crear_Click:'Crear',habilitar_Click:'Habilitar',deshabilitar_Click:'Deshabilitar'};
 for(const f of files('capaVisual').filter(f=>f.endsWith('.cs')&&!f.endsWith('.Designer.cs'))){
  let s=fs.readFileSync(f,'utf8');
  for(const [bad,good]of Object.entries(names))s=s.replaceAll('.'+bad+'(','.'+good+'(');
  fs.writeFileSync(f,s);
 }
}
