CREATE TRIGGER prevent_self_review_delete
ON Review
INSTEAD OF DELETE
AS
BEGIN
   IF EXISTS (SELECT 1 FROM Review r
              INNER JOIN [User] u ON r.id = u.id
              WHERE r.id IN (SELECT id FROM deleted) AND u.id IN (SELECT id FROM deleted))
   BEGIN
      RAISERROR('Cannot delete your own review', 16, 1);
      ROLLBACK TRAN;
   END
   ELSE
   BEGIN
      DELETE FROM Review
      WHERE id IN (SELECT id FROM deleted);
   END
END
--Pokud funkce EXISTS vrátí hodnotu true, znamená to, že se uživatel pokouší odstranit svou vlastní recenzi, takže spouštìè vyvolá chybu a vrátí pøíkaz DELETE zpìt. Pokud funkce EXISTS vrátí false, znamená to, že se uživatel nepokouší smazat vlastní recenzi, takže spouštìè nechá pøíkaz DELETE pokraèovat.

CREATE TRIGGER prevent_low_score_update
ON Review
INSTEAD OF UPDATE
AS
BEGIN
   IF EXISTS (SELECT 1 FROM Review r
              INNER JOIN [User] u ON r.id = u.id
              WHERE r.id IN (SELECT id FROM inserted) AND u.id IN (SELECT id FROM inserted) AND r.rating < 3)
   BEGIN
      RAISERROR('Cannot update review to a score less than 3', 16, 1);
      ROLLBACK TRAN;
   END
   ELSE
   BEGIN
      UPDATE Review
      SET rating = (SELECT rating FROM inserted), comment = (SELECT comment FROM inserted)
      WHERE id IN (SELECT id FROM inserted);
   END
END
--If the review with an 'id' of 5 was created by the same user who is trying to update it and the new score is less than 3, the trigger will raise an error and roll back the UPDATE statement. If the review was created by a different user or the new score is 3 or greater, the UPDATE statement will be allowed to proceed.



CREATE TRIGGER create_review_backup_insert
ON Review
AFTER INSERT
AS
BEGIN
   INSERT INTO Review_Backup ( rating, comment, fk_user_id)
   SELECT  rating, comment, fk_user_id
   FROM inserted;
END

--Tento pøíkaz INSERT vloží do tabulky "Review" nový øádek s "rating" 5, "comment" "New review" a "fk_user_id" 2. Poté bude aktivován trigger "create_review_backup_insert", který vytvoøí zálohu tohoto nového øádku v tabulce "Review_Backup".
INSERT INTO Review VALUES(5,'Test for TRIGGER',4);






