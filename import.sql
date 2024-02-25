USE [DMA-CSD-S212_10407505];

DROP TABLE fp_UserConsent, fp_User, fp_Employee, fp_Cookie, fp_ScanHistory, fp_Domain, fp_Company;
go

CREATE TABLE fp_Company(
  id INT IDENTITY(1,1) NOT NULL,
  name VARCHAR(50) NOT NULL,

  CONSTRAINT PK_company PRIMARY KEY (id)
);

CREATE TABLE fp_Employee(
  id INT IDENTITY(1,1) NOT NULL,
  name VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  phone VARCHAR(50) NOT NULL,
  passwordHash nCHAR(60),
  companyID INT NOT NULL,

  CONSTRAINT FK_Employee_Company FOREIGN KEY (companyID) REFERENCES fp_Company(id) ON DELETE CASCADE,
  CONSTRAINT PK_Employee PRIMARY KEY (id)
);

CREATE TABLE fp_Domain(
  url VARCHAR(50) NOT NULL,
  name VARCHAR(50) NOT NULL,
  companyID INT NOT NULL,

  CONSTRAINT FK_Domain_Company FOREIGN KEY (companyID) REFERENCES fp_Company(id) ON DELETE CASCADE,
  CONSTRAINT PK_Domain PRIMARY KEY (url)
);

CREATE TABLE fp_ScanHistory(
  id INT IDENTITY(1,1) NOT NULL,
  domainURL VARCHAR(50) NOT NULL,
  scanDate VARCHAR(50) NOT NULL,
  cookieAmount INT NOT NULL,

  CONSTRAINT FK_scanHistory FOREIGN KEY (domainURL) REFERENCES fp_Domain(url) ON DELETE CASCADE,
  CONSTRAINT PK_id PRIMARY KEY (id)
);

CREATE TABLE fp_Cookie(
  id INT IDENTITY(1,1) NOT NULL,
  name VARCHAR(50) NOT NULL,
  value VARCHAR(256) NOT NULL,
  expirationDate DATETIME NOT NULL,
  domainURL VARCHAR(50) NOT NULL,
  category VARCHAR(50) NOT NULL,

  CONSTRAINT FK_Cookie_Domain FOREIGN KEY (domainURL) REFERENCES fp_Domain(url) ON DELETE CASCADE,
  CONSTRAINT PK_Cookie PRIMARY KEY (id)
);

CREATE TABLE fp_User(
  browserId VARCHAR(50) NOT NULL,

  CONSTRAINT PK_User PRIMARY KEY (browserId)
);

CREATE TABLE fp_UserConsent(
  date DATETIME NOT NULL,
  userID VARCHAR(50) NOT NULL,
  domainUrl VARCHAR(50) NOT NULL,
  necessary BIT NOT NULL,
  functionality BIT NOT NULL,
  analytics BIT NOT NULL,
  marketing BIT NOT NULL,

  CONSTRAINT FK_UserConsent_User FOREIGN KEY (userID) REFERENCES fp_User(browserId) ON DELETE CASCADE,
  CONSTRAINT FK_UserConsent_Domain FOREIGN KEY (domainUrl) REFERENCES fp_Domain(url) ON DELETE CASCADE,
  CONSTRAINT PK_UserConsent PRIMARY KEY (userID, domainUrl)
);


-- INSERT TEST DATA IN TABLES
INSERT INTO fp_Company (name) VALUES ('Company1');
INSERT INTO fp_Company (name) VALUES ('Company2');
INSERT INTO fp_Company (name) VALUES ('Company3');

INSERT INTO fp_Employee (name, email, phone, passwordHash, companyID) VALUES ('Employee1', 'Employee1@gmail.com', '12345678', '12345678', 1);
INSERT INTO fp_Employee (name, email, phone, passwordHash, companyID) VALUES ('Employee2', 'Employee2@gmail.com', '12345678', '12345678', 1);

INSERT INTO fp_Domain (url, name, companyID) VALUES ('www.company1.com', 'Company1', 1);
INSERT INTO fp_Domain (url, name, companyID) VALUES ('www.company2.com', 'Company2', 2);

INSERT INTO fp_ScanHistory(domainURL, scanDate, cookieAmount) VALUES ('www.company1.com', '2020-01-01', 32);

INSERT INTO fp_Cookie (name, value, expirationDate, domainURL, category) VALUES ('cookie1', 'value1', '2020-01-01', 'www.company1.com', 'category1');
INSERT INTO fp_Cookie (name, value, expirationDate, domainURL, category) VALUES ('cookie2', 'value2', '2020-01-01', 'www.company1.com', 'category2');

INSERT INTO fp_User (browserId) VALUES ('1');

INSERT INTO fp_UserConsent (date, userID, domainUrl, necessary, functionality, analytics, marketing) VALUES ('2020-01-01', '1', 'www.company1.com', 1, 0, 0, 0);
