## API CINEMA_USUARIOS

Essa API foi desenvolvida em .NET, trata-se de uma API que simula o sistema de um cinema, onde pode-se cadastrar 
um cinema e as sessões que são associadas ao mesmo. Também podemos cadastrar em cada cinema vários filmes e um 
gerente responsável por aquele cinema 

# OBS: 

Ao baixar o projeto, você deve atualizar a ConnectionString, colocando os dados do banco de dados que você quer 
se conectar. Também devem gerar novas migrations e atualizar o banco de dados com os comandos (respectivamente):

                        dotnet ef migrations add nomeDaMigration
                        dotnet ef database update 

### CINEMA 

# POST 



