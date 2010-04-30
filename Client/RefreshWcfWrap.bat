rem ##########################
rem # note: le service (BusinessLayer.svc) doit être lancé avant d'utiliser ce script depuis son dossier courrant.
rem ##########################

"c:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\SvcUtil.exe" http://localhost:2900/BusinessLayer.svc?wsdl
del App.config
del BusinessLayerWraper.cs
rename BusinessLayer.cs BusinessLayerWrapper.cs
rename output.config App.config
