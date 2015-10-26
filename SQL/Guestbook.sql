USE Guestbook_Dev

CREATE TABLE Entries
(
EntryId INT Identity (1,1) PRIMARY KEY NOT NULL,

CreatedDate DateTime NOT NULL,

Name VARCHAR(75),

EntryContent VARCHAR(500) NOT NULL,
)