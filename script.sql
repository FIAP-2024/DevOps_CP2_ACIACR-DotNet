-- Script DDL para as tabelas do banco de dados

-- Criação da tabela Addresses
CREATE TABLE Addresses (
    IdEndereco INT IDENTITY(1,1) PRIMARY KEY,
    Logradouro NVARCHAR(MAX) NOT NULL,
    Numero NVARCHAR(MAX) NOT NULL,
    Bairro NVARCHAR(MAX) NOT NULL,
    Cidade NVARCHAR(MAX) NOT NULL,
    SiglaEstado NVARCHAR(MAX) NOT NULL,
    Cep NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

-- Criação da tabela Companies
CREATE TABLE Companies (
    IdEmpresa BIGINT IDENTITY(1,1) PRIMARY KEY,
    RazaoSocial NVARCHAR(MAX) NOT NULL,
    Cnpj NVARCHAR(MAX) NOT NULL,
    Telefone NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    QtdFuncionarios INT NOT NULL,
    Setor NVARCHAR(MAX) NOT NULL,
    AddressIdEndereco INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_Companies_Addresses FOREIGN KEY (AddressIdEndereco) REFERENCES Addresses(IdEndereco) ON DELETE CASCADE
);

-- Criação da tabela Volunteers
CREATE TABLE Volunteers (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(MAX) NOT NULL,
    DtNascimento DATE NOT NULL,
    Genero INT NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    Senha NVARCHAR(MAX) NOT NULL,
    Telefone NVARCHAR(MAX) NOT NULL,
    AddressIdEndereco INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_Volunteers_Addresses FOREIGN KEY (AddressIdEndereco) REFERENCES Addresses(IdEndereco) ON DELETE CASCADE
);

-- Índices
CREATE INDEX IX_Companies_AddressIdEndereco ON Companies (AddressIdEndereco);
CREATE INDEX IX_Volunteers_AddressIdEndereco ON Volunteers (AddressIdEndereco);