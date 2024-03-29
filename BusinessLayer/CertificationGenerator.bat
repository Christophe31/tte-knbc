﻿rem 
rem This command generate a new certificate
rem 
rem It has to be executed (copy pasted) from an admin launched console found in:
rem
rem  Start->AllPrograms->VS2010->VS tools->VS Command Prompt
rem
rem Note: for this operation, you must run the shell as administrator. 
rem 
rem please copy paste these two commands...

makecert -sv SignRoot.pvk -cy authority -r signroot.cer -a sha1 -n "CN=Dev Certification Authority" -ss my -sr localmachine

makecert -iv SignRoot.pvk -ic signroot.cer -cy end -pe -n CN="localhost" -eku 1.3.6.1.5.5.7.3.1 -ss my -sr localmachine -sky exchange -sp "Microsoft RSA SChannel Cryptographic Provider" -sy 12
