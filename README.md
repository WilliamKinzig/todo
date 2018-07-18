Delete Method
Try adding functionality for deleting an individual item now. Luckily it is very similar to the process we used for adding the update functionality. We will start by writing a method to delete a particular item. To help, here is the SQL command you can use in your Delete() method:

DELETE FROM items WHERE id = @thisId;
After we add a delete method to the Item class, it will have complete CRUD functionality!
