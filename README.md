# 🏥 Gerenciamento de Plantão

Sistema web desenvolvido em **C# com ASP.NET Core MVC** para registro e gerenciamento de acionamentos realizados durante plantões de atendimento.

O projeto surgiu a partir de uma necessidade observada em um cenário real: centralizar e organizar informações dos atendimentos realizados durante os plantões, permitindo maior controle e facilitando futuras consultas e análises.

---

## 🚧 Status do projeto

🟡 **Em desenvolvimento ativo**

Novas funcionalidades, regras de negócio e melhorias estruturais continuam sendo implementadas conforme a evolução do projeto.

---

## 📌 Sobre o projeto

O **Gerenciamento de Plantão** permite registrar e administrar os acionamentos realizados durante um plantão, relacionando informações como:

- Usuário responsável
- Data do acionamento
- Estabelecimento
- Setor
- Canal de atendimento
- Categoria do acionamento
- Solução aplicada
- Necessidade de apoio
- Observações e demais informações do atendimento

Além do registro dos acionamentos, a aplicação possui cadastros auxiliares utilizados para organização e padronização das informações.

---

## 🚀 Funcionalidades

### Acionamentos
- Cadastro e gerenciamento de acionamentos
- Associação com estabelecimento, setor, canal, categoria e solução
- Registro do usuário responsável pelo acionamento
- Controle de edição e exclusão de acordo com o usuário responsável

### Cadastros
- Estabelecimentos
- Setores
- Departamentos
- Canais
- Categorias
- Soluções
- Usuários

### Usuários e segurança
- Autenticação utilizando **ASP.NET Core Identity**
- Gerenciamento de usuários
- Perfis de acesso
- Controle de permissões
- Ativação e inativação de registros

---

## 🛠️ Tecnologias utilizadas

- **C#**
- **.NET 8**
- **ASP.NET Core MVC**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQL Server**
- **LINQ**
- **Razor Views**
- **Bootstrap**
- **Git / GitHub**

---

## 🧱 Arquitetura

O projeto utiliza separação de responsabilidades entre diferentes componentes da aplicação:

```text
Controllers
    ↓
Services
    ↓
Entity Framework Core / DbContext
    ↓
SQL Server
