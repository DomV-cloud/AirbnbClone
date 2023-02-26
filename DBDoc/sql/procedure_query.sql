CREATE PROCEDURE get_apartment_reservation_count
(
   @apartment_id INT
)
AS
BEGIN
   SELECT COUNT(r.id) AS reservation_count
   FROM Apartment a
   INNER JOIN Reservation r ON r.id = a.id
   WHERE a.id = @apartment_id;
END

--This EXEC statement will execute the 'get_apartment_reservation_count' procedure and pass in a value of 2 for the '@apartment_id' parameter. 
--The procedure will then return the number of reservations for the apartment with an 'id' of 2.

EXECUTE get_apartment_reservation_count @apartment_id = 2;



CREATE PROCEDURE get_location_review_average
(
   @location_id INT
)
AS
BEGIN
   SELECT AVG(r.rating) AS average_score
   FROM [Location] l
   INNER JOIN Apartment a ON a.id = l.id
   INNER JOIN [User] u ON u.id = a.id
   INNER JOIN Review r ON r.id = u.id
   WHERE l.id = @location_id;
END

--Tento pøíkaz EXEC spustí proceduru "get_location_review_average" a pøedá hodnotu 3 pro parametr "@location_id". 
--Procedura pak vrátí prùmìrné hodnocení recenzí pro umístìní s "id" 3.

EXEC get_location_review_average @location_id = 2;

CREATE PROCEDURE get_user_review_count
(
   @user_id INT
)
AS
BEGIN
   SELECT COUNT(r.id) AS review_count
   FROM [User] u
   INNER JOIN Review r ON r.id = u.id
   WHERE u.id = @user_id;
END

EXECUTE get_user_review_count @user_id = 2;