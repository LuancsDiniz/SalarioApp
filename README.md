# SalarioApp

Este projeto é uma aplicação ASP.NET Web Forms desenvolvida como parte de um desafio técnico. 
O objetivo principal é importar dados de uma planilha Excel para um banco de dados relacional Oracle, realizar cálculos salariais no banco e exibir esses dados em uma interface web.

## Tecnologias Utilizadas

- ASP.NET Web Forms (.NET Framework)
- Oracle Database XE (usado: Oracle 21c XE)
- Oracle.ManagedDataAccess (ODP.NET)
- Visual Studio
- SQL Developer

## Estrutura do Banco de Dados

O banco de dados é composto pelas seguintes tabelas:

### Tabelas de origem

- **cargo**  
  Campos:
  - id : número, chave primária  
  - nome: nome do cargo  
  - salario: valor do salário base

- **pessoa**  
  Campos:
  - id: número, chave primária  
  - nome: nome da pessoa  
  - cidade, email, cep, endereco, pais, usuario, telefone, data_nascimento: dados pessoais  
  - cargo_id: chave estrangeira referenciando cargo(id)

### Tabela de destino

- **pessoa_salario**  
  Campos:
  - pessoa_id: id da pessoa  
  - pessoa_nome: nome da pessoa  
  - cargo_nome: nome do cargo  
  - salario: salário associado ao cargo

## Scripts SQL (Criação e Lógica de Cálculo)

-- Tabela de cargos
CREATE TABLE cargo (
    ID NUMBER PRIMARY KEY,
    Nome VARCHAR2(100) NOT NULL,
    Salario NUMBER(10,2) NOT NULL
);

-- Tabela de pessoas
CREATE TABLE pessoa (
    ID NUMBER PRIMARY KEY,
    Nome VARCHAR2(100) NOT NULL,
    Cidade VARCHAR2(100),
    Email VARCHAR2(100),
    Cep VARCHAR2(20),
    Enderco VARCHAR2(200),
    Pais VARCHAR2(100),
    Usuario VARCHAR2(50),
    Telefone VARCHAR2(20),
    Data_Nascimento DATE,
    Cargo_Id NUMBER,
    CONSTRAINT fk_cargo FOREIGN KEY (Cargo_Id) REFERENCES cargo(ID)
);

-- Tabela de salários
CREATE TABLE pessoa_salario (
    pessoa_id NUMBER PRIMARY KEY,
    pessoa_nome VARCHAR2(100) NOT NULL,
    cargo_nome VARCHAR2(100) NOT NULL,
    salario NUMBER(10,2) NOT NULL,
    CONSTRAINT fk_pessoa FOREIGN KEY (pessoa_id) REFERENCES pessoa(Id)
);

-- Procedure de cálculo/atualização de salários
CREATE OR REPLACE PROCEDURE atualizar_salarios IS
BEGIN
  MERGE INTO pessoa_salario ps
  USING (
    SELECT p.id AS pessoa_id,
           p.nome AS pessoa_nome,
           c.nome AS cargo_nome,
           c.salario
    FROM pessoa p
    JOIN cargo c ON p.cargo_id = c.id
  ) dados
  ON (ps.pessoa_id = dados.pessoa_id)
  WHEN MATCHED THEN
    UPDATE SET
      ps.pessoa_nome = dados.pessoa_nome,
      ps.cargo_nome = dados.cargo_nome,
      ps.salario = dados.salario
  WHEN NOT MATCHED THEN
    INSERT (pessoa_id, pessoa_nome, cargo_nome, salario)
    VALUES (dados.pessoa_id, dados.pessoa_nome, dados.cargo_nome, dados.salario);
END;

-- Executar para popular os dados
EXEC atualizar_salarios;

## Configuração da Conexão

No arquivo Web.config:

xml
<connectionStrings>
  <add name="MinhaConexao" 
       connectionString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=ADMIN2;Password=admin1;" 
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>


> 💡 Altere os dados conforme a configuração do seu Oracle local.

## Funcionalidades da Aplicação

- Exibe os salários das pessoas na tela Salarios.aspx em formato de tabela (GridView).
- Botão "Calcular" executa a procedure 'atualizar_salarios', atualizando a tabela 'pessoa_salario'.
- Mensagem de confirmação ou erro exibida na tela após a execução.

## Como Rodar o Projeto

1. Clone o repositório.
2. Importe os dados da planilha para as tabelas 'cargo' e 'pessoa'.
3. Execute os scripts SQL acima no Oracle XE.
4. Abra o projeto no Visual Studio.
5. Execute a aplicação ('F5').
6. Acesse a tela de salários, clique em "Calcular" e veja os resultados.
