 🔙 [Back to Main Docs](/README.md)  

--- 
# Downloads API

## **Download a File**
**Endpoint:** `GET /api/Downloads/book/{book_id}/{user_id}`  
**Authorization:** Required  

### **Request Parameters**
| Parameter | Type  | Description |
|-----------|-------|-------------|
| `book_id` | `int` | The ID of the book to download. |
| `user_id` | `int` | The ID of the user requesting the download. |

### **Response**
#### **Success (200)**
Returns the requested file for download.

#### **Failure Responses**
- `404 Not Found` - Book or file not found.
- `401 Unauthorized` - If the user is not authorized.

---

## **Get All Download Logs**
**Endpoint:** `GET /api/Downloads/get-all-download-log`  
**Authorization:** Required  

### **Response**
#### **Success (200)**
Returns a list of all download logs.

```json
{
    "downloadLog": [
        {
            "id": 1,
            "bookId": 5,
            "userId": 10,
            "downloadDate": "2024-03-10T12:34:56"
        }
    ]
}
```

#### **Failure Responses**
- `404 Not Found` - If no download logs are available.
- `401 Unauthorized` - If the user is not authorized.
 