 
🔙 [Back to Main Docs](/README.md)  

--- 
# Authentication API

## **Get All Users**
**Endpoint:** `GET /api/Login/get-all-user`  
**Authorization:** Required  

### **Response**
#### **Success (200)**
Returns a list of all users with limited details.

```json
[
    {
        "GroupId": 1,
        "Id": 5,
        "Role": "Admin",
        "Username": "JohnDoe",
        "Password": ""
    }
]
```

#### **Failure Responses**
- `401 Unauthorized` - If the user is not authenticated.

---

## **User Login**
**Endpoint:** `POST /api/Login/login`  
**Content Type:** `application/json` or `multipart/form-data`  

### **Request Parameters**
| Parameter  | Type     | Description            |
|------------|---------|------------------------|
| `username` | `string` | User's login username. |
| `password` | `string` | User's password.       |

### **Response**
#### **Success (200)**
Returns the authenticated user and their token.

```json
{
    "error": "",
    "user": {
        "id": 5,
        "username": "JohnDoe",
        "groupId": 1,
        "role": "Admin",
        "token": "your_jwt_token"
    }
}
```

#### **Failure Responses**
- `400 Bad Request` - Invalid credentials.

---

## **User Registration**
**Endpoint:** `POST /api/Login/register`  
**Content Type:** `application/json` or `multipart/form-data`  

### **Request Parameters**
| Parameter  | Type     | Description                       |
|------------|---------|-----------------------------------|
| `username` | `string` | Desired username.                |
| `password` | `string` | Desired password.                |
| `groupId`  | `int`    | ID of the user's group.          |
| `role`     | `string` | Role of the user (e.g., Admin). |

### **Response**
#### **Success (200)**
Returns the newly created user and their token.

```json
{
    "token": "your_jwt_token",
    "error": "",
    "user": {
        "id": 10,
        "username": "JaneDoe",
        "groupId": 2,
        "role": "User"
    }
}
```

#### **Failure Responses**
- `400 Bad Request` - Missing required fields or user already exists.

---

## **Update User**
**Endpoint:** `POST /api/Login/update-user`  
**Content Type:** `application/json`  

### **Request Parameters**
| Parameter  | Type     | Description                       |
|------------|---------|-----------------------------------|
| `id`       | `int`    | The ID of the user to update.   |
| `username` | `string` | New username (optional).        |
| `password` | `string` | New password (optional).        |
| `groupId`  | `int`    | New group ID (optional).        |
| `role`     | `string` | New role (optional).            |

### **Response**
#### **Success (200)**
Returns the updated user information.

```json
{
    "token": "your_updated_jwt_token",
    "error": "",
    "user": {
        "id": 5,
        "username": "UpdatedUser",
        "groupId": 1,
        "role": "Admin"
    }
}
```

#### **Failure Responses**
- `400 Bad Request` - If `username` is missing.
- `404 Not Found` - If the user does not exist.
 