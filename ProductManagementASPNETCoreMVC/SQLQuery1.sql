CREATE DATABASE MyStore;
GO

USE MyStore;
GO

-- 1. Bảng AccountMember
CREATE TABLE AccountMember (
    MemberID NVARCHAR(20) PRIMARY KEY,
    MemberPassword NVARCHAR(80) NOT NULL,
    FullName NVARCHAR(80) NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL,
    MemberRole INT NOT NULL
);

-- 2. Bảng Categories
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName NVARCHAR(15) NOT NULL
);

-- 3. Bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(40) NOT NULL,
    CategoryID INT NOT NULL,
    UnitsInStock SMALLINT NULL,
    UnitPrice MONEY NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);