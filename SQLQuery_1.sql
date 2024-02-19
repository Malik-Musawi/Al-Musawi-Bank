CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    PasswordHash VARBINARY(MAX),
    PasswordSalt VARBINARY(MAX)
);

CREATE TABLE Accounts (
    AccountId INT PRIMARY KEY IDENTITY,
    UserId INT,
    Balance DECIMAL(18, 2),
    AccountNumber NVARCHAR(100),
    CONSTRAINT FK_Accounts_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY IDENTITY,
    AccountId INT,
    Amount DECIMAL(18, 2),
    Date DATETIME,
    Description NVARCHAR(255),
    CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);

CREATE TABLE Transfers (
    TransferId INT PRIMARY KEY IDENTITY,
    FromAccountId INT,
    ToAccountId INT,
    Amount DECIMAL(18, 2),
    Date DATETIME,
    Description NVARCHAR(255),
    CONSTRAINT FK_Transfers_Accounts_From FOREIGN KEY (FromAccountId) REFERENCES Accounts(AccountId),
    CONSTRAINT FK_Transfers_Accounts_To FOREIGN KEY (ToAccountId) REFERENCES Accounts(AccountId)
);
