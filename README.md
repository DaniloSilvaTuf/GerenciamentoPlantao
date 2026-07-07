## 🏥 Gerenciamento Plantão

Sistema web desenvolvido em **ASP.NET Core MVC** para gerenciamento de plantões em unidades de saúde, permitindo o controle de estabelecimentos, setores e organização de escalas de trabalho.

---

## 🚧 Status do projeto

Este sistema encontra-se em desenvolvimento ativo.

Funcionalidades estão sendo implementadas e aprimoradas diariamente, com foco em boas práticas de arquitetura, organização de código e evolução gradual do sistema.

---

## 📌 Sobre o projeto

O **GerenciamentoPlantao** tem como objetivo facilitar a administração de plantões em hospitais e clínicas, organizando de forma simples e eficiente:

- Estabelecimentos (hospitais/unidades)
- Setores internos (ex: UTI, Centro Cirúrgico, Postos)
- Controle e organização dos plantões

O sistema foi desenvolvido com foco em aprendizado e prática de arquitetura MVC, separação de responsabilidades e boas práticas de desenvolvimento.

---

## 🚀 Funcionalidades

- Cadastro de estabelecimentos
- Cadastro e edição de setores
- Ativação e inativação de registros (soft delete)
- Relacionamento entre setores e estabelecimentos
- Interface web para gerenciamento dos dados
- Estrutura baseada em Service Layer

---

## 🛠️ Tecnologias utilizadas

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

---

## 🧱 Arquitetura do projeto

O projeto segue uma arquitetura em camadas:

Controllers → Services → Data (DbContext) → Models

Essa separação ajuda a manter o código organizado, escalável e fácil de manter.

---

## 🗄️ Banco de dados

O banco de dados é criado automaticamente durante a execução da aplicação.

Não utiliza seed data fixa — todos os registros são inseridos dinamicamente durante o uso e testes da aplicação.

---

## 👨‍💻 Autor

Desenvolvido por Danilo Silva

Projeto criado com foco em estudo e evolução no desenvolvimento backend com ASP.NET Core MVC.
