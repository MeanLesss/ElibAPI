🔙 [Back to Main Docs](../README.md)  

--- 

# **Groups API Documentation**

This document provides details about the available API endpoints for managing groups in the **ELibAPI** system.

## **Base URL:**
```
/api/Groups
```

## **Endpoints**

### 1. **Add a New Group**
- **Endpoint:** `POST /api/Groups/add-group`
- **Description:** Adds a new group.
- **Authorization:** Required (JWT Token)
- **Consumes:** `application/x-www-form-urlencoded` or `application/json`

#### **Request Example:**
```http
POST /api/Groups/add-group HTTP/1.1
Host: yourapi.com
Content-Type: application/json
Authorization: Bearer <your-jwt-token>

{
    "Name": "Mathematics Group"
}
```

#### **Response Example:**
```json
{
    "status": true,
    "message": "Group successfully added!"
}
```

#### **Response Code:**
- `200 OK` - Successfully added the group.
- `400 Bad Request` - Group already exists or missing data.

---

### 2. **Update an Existing Group**
- **Endpoint:** `POST /api/Groups/update-group`
- **Description:** Updates the details of an existing group.
- **Authorization:** Required (JWT Token)

#### **Request Example:**
```http
POST /api/Groups/update-group HTTP/1.1
Host: yourapi.com
Content-Type: application/json
Authorization: Bearer <your-jwt-token>

{
    "group_id": 1,
    "name": "Updated Group Name",
    "group_for": "High School"
}
```

#### **Response Example:**
```json
{
    "status": true,
    "message": "Group successfully updated!"
}
```

#### **Response Code:**
- `200 OK` - Successfully updated the group.
- `400 Bad Request` - Group not found or name already exists.

---

### 3. **Add a Teacher to a Group**
- **Endpoint:** `POST /api/Groups/add-teacher-to-group`
- **Description:** Adds a teacher to a group.
- **Authorization:** Required (JWT Token)

#### **Request Example:**
```http
POST /api/Groups/add-teacher-to-group HTTP/1.1
Host: yourapi.com
Content-Type: application/json
Authorization: Bearer <your-jwt-token>

{
    "teacher_id": 5,
    "group_id": 2
}
```

#### **Response Example:**
```json
{
    "status": true,
    "message": "Teacher successfully added to Mathematics Group!"
}
```

#### **Response Code:**
- `200 OK` - Successfully added the teacher to the group.
- `400 Bad Request` - Invalid user data or group not found.

---

### 4. **Delete a Group**
- **Endpoint:** `DELETE /api/Groups/{group_id}`
- **Description:** Deletes a group by its ID.
- **Authorization:** Required (JWT Token)

#### **Request Example:**
```http
DELETE /api/Groups/3 HTTP/1.1
Host: yourapi.com
Authorization: Bearer <your-jwt-token>
```

#### **Response Example:**
```json
{
    "message": "Successfully deleted!"
}
```

#### **Response Code:**
- `200 OK` - Successfully deleted the group.
- `404 Not Found` - Group not found.

---

## **Error Codes**
- `400 Bad Request` - The request is invalid, or some required fields are missing.
- `401 Unauthorized` - Authentication failed, or no valid token provided.
- `404 Not Found` - The specified resource could not be found.
- `500 Internal Server Error` - There was an unexpected error processing the request.

---

### **Notes**
- Ensure the **Authorization header** is included in all requests requiring authentication.
- Group names should be unique to avoid conflicts.
- Only users with the necessary permissions can add or remove groups.

 