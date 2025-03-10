CREATE DATABASE "ELibrary";

USE "ELibrary";

CREATE TABLE "users" (
	"id" INTEGER IDENTITY(1,1) NOT NULL,
	"username"	NVARCHAR(225) NOT NULL,
	"pwd"	NVARCHAR(225) NOT NULL,
	"role"	NVARCHAR(225) NOT NULL DEFAULT 'Student',
	"token"	NVARCHAR(225),
	"group_id" INTEGER,
	"remote_addr" NVARCHAR(225),
	"forward_addr" NVARCHAR(225),
    "image" NVARCHAR(225),
	PRIMARY KEY("id")
);


CREATE TABLE "groups" (
	"id" INTEGER IDENTITY(1,1) NOT NULL,
	"name"	NVARCHAR(225),
	"group_for"	NVARCHAR(225),
	PRIMARY KEY("id")
);
CREATE TABLE "teachers_groups" (
	"id" INTEGER IDENTITY(1,1) NOT NULL,
	"teacher_id"	INTEGER,
	"group_id"	INTEGER,
	PRIMARY KEY("id")
);
CREATE TABLE "books" (
	"id" INTEGER IDENTITY(1,1) NOT NULL,
	"title"	NVARCHAR(225),
	"path"	NVARCHAR(225),
	"user_id"	INTEGER,
	"group_id"	INTEGER,
	PRIMARY KEY("id")
);
CREATE TABLE "downloads" (
    "id" INTEGER IDENTITY(1,1) NOT NULL,
    "user_id"	INTEGER,
    "book_id"	INTEGER,
    PRIMARY KEY("id")
);

