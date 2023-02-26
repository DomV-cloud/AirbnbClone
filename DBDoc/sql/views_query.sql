--vytvoøení pohledu reservation_count
CREATE VIEW reservation_count AS
SELECT a.id AS apartment_id, COUNT(r.id) AS 'poèet rezervací'
FROM Apartment a
LEFT JOIN Reservation r ON r.id = a.id
GROUP BY a.id;

--Jak využít reservation_count view:
SELECT * FROM reservation_count;
--Zobrazí se seznam všech apartmánù a poèet rezervací pro každý apartmán.

--vytvoøení pohledu user_photo_count
CREATE VIEW user_photo_count AS
SELECT  u.id AS 'id',u.[name] AS 'uživatel',  COUNT(p.id) AS photo_count
FROM [User] u
LEFT JOIN Apartment a ON a.id = u.id
LEFT JOIN Photo p ON p.id = a.id
GROUP BY u.id,u.[name]
ORDER BY 'uživatel' ASC
OFFSET 0 ROWS;

--Jak využít user_photo_count view:
SELECT * FROM user_photo_count;
--Vrátí se seznam všech uživatelù spolu s poètem fotografií pro každého uživatele, seøazených vzestupnì podle sloupce 'uživatel'.

--vytvoøení pohledu apartment_review_scores
CREATE VIEW apartment_review_scores AS
SELECT TOP (100) PERCENT a.id AS apartment_id, a.name_apartment AS 'název rezortu', AVG(r.rating) AS 'hodnocení'
FROM Apartment a
LEFT JOIN [User] u ON u.id = a.id
LEFT JOIN Review r ON r.id= u.id
GROUP BY a.id, a.name_apartment;

--Jak využít apartment_review_scores view:
SELECT * FROM apartment_review_scores;
--Zobrazí se seznam všech apartmánù a prùmìrné hodnocení jednotlivých apartmánù.


