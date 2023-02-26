CREATE DATABASE Airbnb;
USE Airbnb;

--Vytvoøení Tabulky typ uživatele
CREATE TABLE Type_of_user(
id INT PRIMARY KEY IDENTITY(1,1),
user_type VARCHAR(20) NOT NULL
);

INSERT INTO Type_of_user VALUES('owner');
INSERT INTO Type_of_user VALUES('costumer');

SELECT * FROM Type_of_user;

CREATE TABLE [User](
id INT PRIMARY KEY IDENTITY(1,1),
[name] VARCHAR(50) NOT NULL CHECk([name] LIKE '[A-Za-z]%'),
second_name VARCHAR(50) NOT NULL,
email VARCHAR(50) NOT NULL CHECK(email LIKE '%@%'),
[password] VARCHAR(50) NOT NULL,
fk_type_of_user_id int NOT NULL FOREIGN KEY REFERENCES Type_of_user(id)
);


insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Lucienne', 'Prewer', 'lprewer0@fema.gov', '1EE8DucO3Qz', 1);
insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Rob', 'Rosell', 'rrosell1@google.co.uk', 'nqw3xNa7RAVx', 1);
insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Tiffy', 'Olivella', 'tolivella2@blogs.com', '8WPTIMhBvsc', 1);
insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Deidre', 'Whatmough', 'dwhatmough3@hibu.com', 'fIXrXlqp5a61', 2);
insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Jandy', 'Girardengo', 'jgirardengo4@dion.ne.jp', 'pN6y2a9', 1);
insert into [User] ([name], second_name, email, [password], fk_type_of_user_id) values ('Peter', 'Griffing', 'pgriff@outlook.com', 'HGFDS54h', 2);

SELECT * FROM [User];

CREATE TABLE Review(
id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
rating DECIMAL NOT NULL CHECK(rating BETWEEN 1 AND 5),
comment VARCHAR(250) NOT NULL,
fk_user_id int NOT NULL FOREIGN KEY REFERENCES [User](id)
);

INSERT INTO Review VALUES(4,'Really nice place with beatiful view.', 1);
INSERT INTO Review VALUES(1,'The worst place I have ever been, I had diarrhea after lunch!',2);
INSERT INTO Review VALUES(3.5,'Our weekend in Flow was a real adventure and experience. The view of nature is really unique. Since we were there in the off season, there was no elevator available, which we thought was a positive. ',2);
INSERT INTO Review VALUES(5,'Great place, nice view, nice host. ',6);
INSERT INTO Review VALUES(5,'Great mini house in a great location, minimalist yet detailed love it, will be back :)',4);
INSERT INTO Review VALUES(5,'Great mini house in a great location, minimalist yet detailed love it, will be back :)',4),
(3,'Nice place, good loacation',2),
(5, 'Excellent apartment, wonderful view',3),
(2,'Average apartment, needs impovement',4),
(4,'Good value for the money',4)
;



SELECT * FROM Review;

CREATE TABLE Apartment(
id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
name_apartment VARCHAR(50) NOT NULL,
fk_apartment_photo_id int NOT NULL FOREIGN KEY REFERENCES [Photo](id),
fk_apartment_location_id int NOT NULL FOREIGN KEY REFERENCES [Location](id),
size DECIMAL NOT NULL CHECK(size > 0),
apartment_price DECIMAL NOT NULL,
fk_apartment_review_id int NOT NULL FOREIGN KEY REFERENCES [Review](id),
apartment_info VARCHAR(200) NOT NULL,
number_of_guests INTEGER NOT NULL CHECK(number_of_guests > 0),
number_of_bedroom INTEGER NOT NULL CHECK(number_of_bedroom > 0 ),
number_of_beds INTEGER NOT NULL CHECK(number_of_beds > 0 ),
number_of_bathrooms INTEGER NOT NULL CHECK(number_of_bathrooms > 0),
fk_apartment_user_owner_id int NOT NULL FOREIGN KEY REFERENCES [User](id),

);



INSERT INTO Apartment VALUES('Villa Goyen',1,4,600,53036,1,'The new Villa Goyen - The View House impresses with its exclusive location and unique panoramic views of the surrounding mountain panorama and the Merano River Valley. Large window areas.',5,3,3,2,1);
INSERT INTO Apartment VALUES('Holzhütte',3,4,448,3966,3,'Large, heated cabins have 4-6 beds and a covered terrace. A sleeping bag or bedding can be brought or rented for 5.',6,1,6,1,2);
INSERT INTO Apartment VALUES('Holiday home in Geierswalde with roof terrace',2,5,'495',36081,2,'Luxury bathing holidays: floating holiday homes and apartments in a breathtaking location on the Geierswalder See. Modern architecture and stylish interiors .',3,1,2,1,5);
INSERT INTO Apartment VALUES('Art house in a garden',4,4,650,21180 ,4,'The house is situated in a big art garden full of stone and metal statues,other artifacts,next to the main house. Near Prague center. 2 minutes to a bus stop. Close to metro (subway) station.',3,1,4,2,1);
INSERT INTO Apartment VALUES('My Eldorado',5,5,690,12103 ,5,'The new Villa Goyen - The View House impresses with its exclusive location and unique panoramic views of the surrounding mountain panorama and the Merano River Valley. Large window areas.',5,2,4,1,5);

SELECT * FROM Apartment;

CREATE TABLE Photo(
id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
name_of_photo VARCHAR(50) NOT NULL,
fk_author_user_id int NOT NULL FOREIGN KEY REFERENCES [User](id),
day_of_acqusittion DATE NOT NULL,
photo_path VARCHAR(100) NULL

);


INSERT INTO Photo VALUES('kitchen', 1, '2/3/2022','/Sources/images/Apartments/apartment_Wood/1.jpg                                                     ');
INSERT INTO Photo VALUES('garden', 1, '2/3/2022','/Sources/images/Apartments/apartment_Brick/brick.jpg                                                ');
INSERT INTO Photo VALUES('bedroom', 1, '2/3/2022','/Sources/images/Apartments/apartment_Polar/polar.jpg                                                ');
INSERT INTO Photo VALUES('toilet', 1, '2/3/2022','/Sources/images/Apartments/apartment_TreeHouse/treehouse.jpg                                        ');
INSERT INTO Photo VALUES('kitchen', 2, '2/4/2022','/Sources/images/Apartments/apartment_Mountain/mountain.jpg                                          ');
INSERT INTO Photo VALUES('garden', 2, '2/4/2022','/Sources/images/Apartments/apartment_Wood/1.jpg                                                     ');

SELECT * FROM Photo;

CREATE TABLE [Location](
id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
name_of_location VARCHAR(50) NOT NULL,
city VARCHAR(50) NOT NULL CHECK(city LIKE '[A-Za-z]%'),
[state] VARCHAR(50) NOT NULL CHECK([state]LIKE '[A-Za-z]%'),
info VARCHAR(250) NOT NULL
);



INSERT INTO [Location] VALUES('Elsterheide', 'Sachsen', 'Germany', 'Luxury bathing holidays: floating holiday homes and apartments in a breathtaking location on the Geierswalder See. ');
INSERT INTO [Location] VALUES('Villa Goyen', 'Schenna', 'Italy', 'The new Villa Goyen - The View House impresses with its exclusive location and unique panoramic views of the surrounding mountain panorama ');
INSERT INTO [Location] VALUES('Holzhütte ', 'Schenna', 'Germany', 'Large, heated cabins have 4-6 beds and a covered terrace. A sleeping bag or bedding can be brought or rented for 5,-. No piping in the cabins! Communal bathroom facilities are about 20 to 40 feet away on foot.');
INSERT INTO [Location] VALUES('Art house in a garden', 'Prague', 'Czech republic', 'The house is situated in a big art garden full of stone and metal statues,other artifacts,next to the main house. Near Prague center. 2 minutes to a bus stop. Close to metro (subway) station. 20 minutes to the center of Prague. ');
INSERT INTO [Location] VALUES('Holiday homes between the lakes', 'Sieraków', 'Poland', 'If you like the sound of trees and birds singing, this is the perfect place for you. Especially for families with children.');

SELECT * FROM [Location] ;

CREATE TABLE Reservation(
id int NOT NULL PRIMARY KEY IDENTITY(1,1),
fk_apartment_id int NOT NULL FOREIGN KEY REFERENCES [Apartment](id),
fk_apartment_costumer_id int NOT NULL FOREIGN KEY REFERENCES [User](id),
reservation_since DATE NULL,
reservation_to DATE NULL,
);

INSERT INTO Reservation VALUES(4,4,'2/3/2022', '7/3/2022');

INSERT INTO Reservation VALUES(2,6,'8/5/2022', '10/5/2022');

INSERT INTO Reservation VALUES(4,4,'2/4/2022', '7/4/2022');

INSERT INTO Reservation VALUES(4,3,'8/5/2022', '10/5/2022');

INSERT INTO Reservation VALUES(2,1,'2/3/2022', '7/3/2022');

INSERT INTO Reservation VALUES(2,6,'8/7/2022', '10/7/2022');

SELECT * FROM Reservation;


