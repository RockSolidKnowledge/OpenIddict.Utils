# Generate Version Variable from SourceBranch

perl -version

echo 'Triggered using \'${{Build.SourceBranch}}\''

echo 'Triggerd Version: ${{Build.SourceBranch}}' |  perl -pe 'if(($v)=/([0-9]+([.][0-9]+)+)/){print"$v\n";exit}$_=""'

TaggedVersion=$(echo '${{Build.SourceBranch}}' |  perl -pe 'if(($v)=/([0-9]+([.][0-9]+)+)/){print"$v\n";exit}$_=""')

echo 'Tagged Version ${{TaggedVersion}}'
