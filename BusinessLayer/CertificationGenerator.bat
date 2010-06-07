rem 
rem This command generate a new certificate
rem

makecert -sv SignRoot.pvk -cy authority -r signroot.cer -a
    sha1 -n "CN=localhost" -ss my -sr localmachine