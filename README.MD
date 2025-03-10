# SU25 ELibrary API final project
This API is made for retrieving and categorize books and users to a group to share file as a part of school management.



# **Before getting stated**

 1. Please find run the sql to create datebase first as below
 **[Create Datebase Sql](create%20table-script.sql)**
 2. Update `appsettiing.json` configure database to you endpoint.
   
 ```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=kang_sokvimean-library.db",
    "LibDbContext": "Data Source=LAPTOP-QFUA5EK6\\SQLEXPRESS;Initial Catalog=ELibrary;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  },
 ```


## Application usage:

### Before be able to query any date or perform any CRUD action.
1. User login user to aquire the token_key with user existed in the data base.
   1. If the user is not created yet:
      1. Got Register endpoint as post method.You can learn more from here:   [Authentication APIs-User registration](docs/LoginAPI.md/#user-registration)
      2. As the user created successfully, copy the token and paste in the Authourize box that you can find on the top of the page.
2. Perform any action in SWAGGER.
---

### **📜 Main Navigation (`README.md`)** 
# 📚 ELibAPI Documentation

Welcome to the **ELibAPI** documentation! Below are links to different API categories.

## 🔗 API Route Navigation
1. **[Authentication APIs](docs/LoginAPI.md)**
   - **[Authentication APIs - Login](docs/LoginAPI.md/#user-login)**
   - **[Authentication APIs - Register](docs/LoginAPI.md/#user-registration)**
   - **[Authentication APIs - Update User](docs/LoginAPI.md/#update-user)**

2. **[Books APIs](docs/BookAPI.md)**
   - **[Books APIs - Get All Books](docs/BookAPI.md/#1-get-all-books)**
   - **[Books APIs - Get Book by ID](docs/BookAPI.md/#2-get-a-specific-book)**
   - **[Books APIs - Create Book](docs/BookAPI.md/#3-create-a-new-book)** 
   - **[Books APIs - Update Book](docs/BookAPI.md/#4-update-an-existing-book)** 
   - **[Books APIs - Delete Book](docs/BookAPI.md/#5-delete-a-book)**

3. **[Groups APIs](docs/GroupAPI.md)**
   - **[Groups APIs - Create Group](docs/GroupAPI.md/#1-add-a-new-group)**
   - **[Groups APIs - Update Group](docs/GroupAPI.md/#2-update-an-existing-group)**
   - **[Groups APIs - Add Teacher to Group](docs/GroupAPI.md/#3-add-a-teacher-to-a-group)**
   - **[Groups APIs - Delete Group](docs/GroupAPI.md/#4-delete-a-group)**

4. **[Downloads APIs](docs/DownloadAPI.md)** 
   - **[Downloads APIs - Download File](docs/DownloadAPI.md/#download-a-file)**  
   - **[Downloads APIs - Get Download Logs](docs/DownloadAPI.md/#get-all-download-logs)**

6. **[Summary APIs](docs/SummaryAPI.md)**
   - **[Summary APIs - Get All Summary ](docs/SummaryAPI.md/#get-all-summary)**