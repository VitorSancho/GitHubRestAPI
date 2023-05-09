# GitHubRestAPI

O projeto possui três endpoints:

1 - UpdateFamousRepositoryListFromLanguages:
  Coleta dados dos repositórios com mais estrelas recebidas no GitHub, para uma dada linguagem de programação. 
  É passado como payload uma lista de linguagens de programação. Para cada uma dessas linguagens, a coleta é executada.
  Pode ser passado no máximo 5 linguagens no payload do request.
  
2 - GetCollectedRepositories:
  Para cada repositório coletado no endpoint 1, é retornada um registro com informações mais básicas.
  Cada cada repostiório são retornados os seguites campos: id, name, full name, language, owner login and created_at
  
3 - GetCollectedRepositoriesDetails:
  Para cada repositório coleta no endpoint 2, é retornado seu registro contendo informações mais detalhadas.
  
# Como executar o projeto?

Para executar o projeto é necessário criar o banco de dados. Esse projeto não possui migrations, então o banco de dados deve ser criado utilizando scripts. Os scripts de criação do banco e de sua tabela podem ser encontrados em https://github.com/VitorSancho/GitHubRestAPI/tree/main/GitHub.Data/Scripts.
Além disso, é necessário alterar a string de conexão com o banco, de forma que aponte para o banco que foi criado na etapa anterior. A strign de conexão se encontra em: https://github.com/VitorSancho/GitHubRestAPI/blob/main/GitHubRestAPI/appsettings.json

No mais, qualquer dúvida sobre a execução do projeto pode entrar em contato comigo, utilizando meu email vitor.sancho07@gmail.com
