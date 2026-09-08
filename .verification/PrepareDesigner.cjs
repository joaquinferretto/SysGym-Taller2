const fs=require('fs'),cp=require('child_process'),path=require('path');
const mode=process.argv[2];
const root='.verification/designer-'+mode;
if(fs.existsSync(root))throw Error('La copia de diseño ya existe: '+root);
const tracked=cp.execFileSync('git',['ls-files','-z'],{encoding:'utf8'}).split('\0').filter(Boolean);
for(const f of tracked.filter(f=>/^(capaVisual|capaLogica|capaDatos|Properties)\//.test(f)||/^(Program.cs|App.config|exxen2.0.csproj|exxen2.0.slnx)$/.test(f))){
 const target=path.join(root,f);fs.mkdirSync(path.dirname(target),{recursive:true});
 fs.writeFileSync(target,mode==='before'?cp.execFileSync('git',['show','HEAD:'+f],{maxBuffer:2000000}):fs.readFileSync(f));
}
console.log(root);
