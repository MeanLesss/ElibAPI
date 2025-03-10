 🔙 [Back to Main Docs](/README.md)  

--- 
# Summary API

## **Get All Summary**
**Endpoint:** `GET /api/Summary`  
**Authorization:** Required  
**Description:** Retrieves a summary of statistics based on the role of the user (Librarian or Teacher).

### **Request Parameters**
| Parameter  | Type  | Description            | Required |
|------------|------|------------------------|----------|
| `user_id`  | `int` | The ID of the user requesting the summary. | ✅ |

---

### **Response**
#### **Success (200) - Librarian**
If the user is a **Librarian**, the response contains a detailed summary:

```json
{
    "groups": 5,
    "books": 120,
    "students": 200,
    "teachers": 10,
    "librarian": 3,
    "downloads": 50,
    "status": "SUCCESS",
    "error": ""
}
```

#### **Success (200) - Teacher**
If the user is a **Teacher**, the response includes a limited summary:

```json
{
    "books": 120,
    "groups": 5,
    "status": "SUCCESS",
    "error": ""
}
```

#### **Failure Responses**
- `400 Bad Request` - If the `user_id` is invalid (not found or not authorized).

```json
{
    "status": "FAILED",
    "error": "user_id is invalid."
}
```

---

### **Notes**
- **Librarian** users get a full breakdown of books, groups, students, teachers, librarians, and downloads.
- **Teacher** users get a limited summary containing books and groups.
- This endpoint requires **Authorization**.
 