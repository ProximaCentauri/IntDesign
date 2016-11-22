echo off
rem example of setting the email is \"testing@gmail.com\"
set adminEmail=\"admin@email.com\"
set guest1Email=\"guest1@email.com\"
set guest2Email=\"guest2@email.com\"

set sqlstmt=update appusers set EmailAddress=%adminEmail% where UserName=\"Admin\"
mysql -u root -pjasper -Ddbintdesign -ANe"%sqlstmt%"
set sqlstmt=update appusers set EmailAddress=%guest1Email% where UserName=\"Guest1\"
mysql -u root -pjasper -Ddbintdesign -ANe"%sqlstmt%"
set sqlstmt=update appusers set EmailAddress=%guest2Email% where UserName="\"Guest2\"
mysql -u root -pjasper -Ddbintdesign -ANe"%sqlstmt%"

set adminEmail=%adminEmail:\=%
set guest1Email=%guest1Email:\=%
set guest2Email=%guest2Email:\=%
echo Admin email is updated successfully to %adminEmail% 
echo Guest1 email is updated successfully to %guest1Email%
echo Guest2 email is updated successfully to %guest2Email%