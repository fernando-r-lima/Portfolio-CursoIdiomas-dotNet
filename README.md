# CRUD Curso de Idiomas - .NET 

Este foi meu primeiro projeto para aprender, por mim mesmo, a desenvolver na plataforma .NET.

É uma aplicação web para um curso de idiomas fictício, envolvendo as operações CRUD num banco de dados com cinco tabelas e relacionamentos um-para-muitos e muitos-para-muitos.

<img alt="Diagrama" src="/uml.PNG">

---------

No vídeo abaixo apresento o projeto rodando e seus recursos disponíveis:

<a href="https://www.youtube.com/watch?v=Y_02P4R_RL8">
<img alt="Thumbnail do video" src="/thumbVideo3.png">
</a>

<p>
</br>
A conclusão desse projeto e o aprendizado de todos os conceitos envolvidos teve muita base na própria documentação Microsoft, assim como em cursos livres de C# e na busca de soluções próprias com minhas experimentações de código.
</p>

A partir dele pude estudar, compreender e aplicar diversos conceitos e técnicas. Dentre eles (e um exemplo de código):
- **Framework ASP.NET Core**
- **Padrão MVC**
- **Conexão com banco de dados via EF Core** - ([CursoIdiomasDbContext.cs](/Data/CursoIdiomasDbContext.cs))
- **Migrations Code-first**
- **Injeção de dependência** - ([AlunoController.cs](/Controllers/AlunoController.cs) - Linha 16)
- **Implementação de cada operação CRUD** - ([AlunoController.cs](/Controllers/AlunoController.cs))
- **SQL**
- **Git**
- **HTML** - ([/Views](/Views))
- **Bootstrap** - ([\_Layout.cshtml](/Views/Shared/_Layout.cshtml) - Linha 7)
- **LINQ** - ([TurmaController.cs](/Controllers/TurmaController.cs) - Linha 112)
- **View Models** - ([TurmaController.cs](/Controllers/TurmaController.cs) - Linha 96)
- **Operações assíncronas** - ([ProfessorController.cs](/Controllers/ProfessorController.cs) - Linha 108)
- **Validação com Data Annotations** - ([Aluno.cs](/Models/Aluno.cs))
- **Format Strings** - ([Inscricao.cs](/Models/Inscricao.cs) - Linha 18)
- **Tipo Nullable** - ([ProfessorController.cs](/Controllers/ProfessorController.cs) - Linha 133)
- **Fluent API** - ([CursoIdiomasDbContext.cs](/Data/CursoIdiomasDbContext.cs) - Linha 18)

O projeto foi todo feito de forma incremental. Desde a criação das primeiras classes de maneira bem simples e da conexão ao banco de dados, até a implementação das linhas de código mais complexas. 

Toda essa construção em etapas foi sendo registrada em pequenos commits com a ferramenta Git a fim de se entender o que estava sendo feito e de ter o projeto como uma referência organizada de cada funcionalidade que aprendi como implementar aqui.

---------

Abaixo seguem meu email e perfil no linkedIn para contato:

- fernando.ilustrar@gmail.com
- [LinkedIn](https://www.linkedin.com/in/fernando-rezende-lima)







