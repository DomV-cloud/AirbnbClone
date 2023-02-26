--vytvo�en� pohledu reservation_count
CREATE VIEW reservation_count AS
SELECT a.id AS apartment_id, COUNT(r.id) AS 'po�et rezervac�'
FROM Apartment a
LEFT JOIN Reservation r ON r.id = a.id
GROUP BY a.id;

--Jak vyu��t reservation_count view:
SELECT * FROM reservation_count;
--Zobraz� se seznam v�ech apartm�n� a po�et rezervac� pro ka�d� apartm�n.

--vytvo�en� pohledu user_photo_count
CREATE VIEW user_photo_count AS
SELECT  u.id AS 'id',u.[name] AS 'u�ivatel',  COUNT(p.id) AS photo_count
FROM [User] u
LEFT JOIN Apartment a ON a.id = u.id
LEFT JOIN Photo p ON p.id = a.id
GROUP BY u.id,u.[name]
ORDER BY 'u�ivatel' ASC
OFFSET 0 ROWS;

--Jak vyu��t user_photo_count view:
SELECT * FROM user_photo_count;
--Vr�t� se seznam v�ech u�ivatel� spolu s po�tem fotografi� pro ka�d�ho u�ivatele, se�azen�ch vzestupn� podle sloupce 'u�ivatel'.

--vytvo�en� pohledu apartment_review_scores
CREATE VIEW apartment_review_scores AS
SELECT TOP (100) PERCENT a.id AS apartment_id, a.name_apartment AS 'n�zev rezortu', AVG(r.rating) AS 'hodnocen�'
FROM Apartment a
LEFT JOIN [User] u ON u.id = a.id
LEFT JOIN Review r ON r.id= u.id
GROUP BY a.id, a.name_apartment;

--Jak vyu��t apartment_review_scores view:
SELECT * FROM apartment_review_scores;
--Zobraz� se seznam v�ech apartm�n� a pr�m�rn� hodnocen� jednotliv�ch apartm�n�.


