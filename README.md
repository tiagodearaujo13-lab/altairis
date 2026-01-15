# 🏨 Altairis Backoffice - MVP de Gestão Hoteleira

Bem-vindo ao repositório do MVP desenvolvido para a **Altairis**.
Este projeto é uma solução Fullstack completa para a gestão de hotéis, inventário e reservas, desenvolvida com tecnologias modernas e containerizada para fácil distribuição.

## 🚀 Como Rodar o Projeto (Quick Start)

Graças à infraestrutura Docker, pode rodar toda a solução (Backend + Frontend + Base de Dados) com **apenas um comando**.

### Pré-requisitos
* Docker e Docker Compose instalados.

### Passo a Passo
1. Clone este repositório:
   ```bash
   git clone [https://github.com/tiago/altairis.git](https://github.com/tiago/altairis.git)
   cd altairis-backoffice
Execute o comando mágico:

Bash

docker-compose up --build
Aceda à aplicação:

Frontend (Backoffice): http://localhost:3000

Backend (Swagger API): http://localhost:5000/swagger

🛠️ Tecnologias Utilizadas
Este projeto foi construído seguindo uma arquitetura robusta e escalável:

Backend: .NET 8 (C#) - Web API RESTful.

Frontend: Next.js 15 (React) - Interface moderna e responsiva.

Base de Dados: PostgreSQL 15 - Persistência de dados relacional.

Infraestrutura: Docker & Docker Compose - Orquestração de contentores.

✅ Funcionalidades Implementadas
O sistema cumpre todos os requisitos funcionais propostos no desafio:

Gestão de Hotéis: Listagem e detalhe de unidades hoteleiras.

Gestão de Quartos: Visualização de tipos de quartos (Suites, Standard, etc.).

Controlo de Disponibilidade: Sistema inteligente que verifica stock real antes de permitir reservas.

Motor de Reservas: Criação de reservas reais com validação de datas e dados do hóspede.

📂 Estrutura do Projeto
/backend - API em .NET Core (Clean Architecture simplificada).

/frontend - Aplicação Next.js com App Router e Tailwind CSS.

docker-compose.yml - Orquestração dos serviços.

Desenvolvido por Tiago Araújo.


---

### 📝 Como adicionar ao GitHub

Tens duas formas de fazer isto:

**Opção A: Pelo Site do GitHub (Mais Fácil)**
1.  Vai ao teu repositório no GitHub.
2.  Clica no botão verde **"Add a README"** (que aparece na tua imagem).
3.  Cola o texto que te dei acima.
4.  Clica em **"Commit changes..."** no canto superior direito.

**Opção B: Pelo teu Terminal (Como um Pro)**
1.  Na pasta raiz do projeto (`altairis-backoffice`), cria um ficheiro chamado `README.md`.
2.  Cola o texto lá dentro e salva.
3.  No terminal, corre:
    ```bash
    git add README.md
    git commit -m "docs: Adiciona documentação do projeto"
    git push
    ```

Diz-me quando estiver feito para eu te ajudar a escrever o **email final** para a empresa! ✉️
