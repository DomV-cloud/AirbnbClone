CREATE LOGIN spravce WITH PASSWORD = 'Heslo123';
CREATE USER spravce for login spravce;
ALTER ROLE db_owner ADD MEMBER spravce;


CREATE LOGIN zam WITH PASSWORD = 'Heslicko123';
CREATE USER zam for login zam;
GRANT SELECT, INSERT, UPDATE, DELETE ON Apartment TO zam;
GRANT SELECT, INSERT, UPDATE, DELETE ON Reservation TO zam;



CREATE LOGIN anal WITH PASSWORD = 'Aars123';
CREATE USER anal for login anal;
GRANT SELECT ON Apartment TO anal;
GRANT SELECT ON Review TO anal;


CREATE LOGIN marketer WITH PASSWORD = 'Mars123';
CREATE USER marketer for login marketer;
GRANT SELECT ON Apartment TO marketer;
