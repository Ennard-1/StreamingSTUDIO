## Services

### Token Service

> O app deve armazenar de forma persistente, o token jwt para acessar a api, contendo funções CRUD para o token e sendo acessivel para a api service

### Api service

> Service que gerencia todas as conexões com a api, facilitando a centralização de informações

Rotas da api a serem utilizadas:

-  post /api/Auth/register
Recebe:
{
  "nome": "string",
  "email": "string",
  "senha": "string"
}

Retorna:

- post /api/Auth/login
Recebe:

{
  "email": "string",
  "senha": "string"
}

Retorna:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzdHJpbmciLCJleHAiOjE3MzAzMTkyOTUsImlzcyI6IlN0cmVhbWluZ0FQSSIsImF1ZCI6IlN0cmVhbWluZ0F1ZGllbmNlIn0.jiog6IuDwtIucNjeyAMIHqT9lsNUa3PTqV7gkQdzII0"
}

- get /api/Auth/info

Recebe: token

Retorna {"id":1,"nome":"string","email":"string"}

- /api/Conteudo/usuario
> rota que retorna conteudo que pertence apenas ao usuario

Recebe: token

Retorna:
[{"id":2,"titulo":"teste","tipo":"video","nomeArquivo":"fc6edf9b-d450-4691-8bef-519c2799d402.mp4","thumbnail":"df6d87b7-783e-49f1-985a-f830c7842ec9.jpg","usuarioID":1}]

- /api/Conteudo/thumbnails/{thumbnail}
> na rota se coloca o nome e extensão do arquivo de imagem e recebe uma imagem estatica da mesma

- /api/Conteudo/stream/{fileName}
> na rota se coloca o nome e extensão do arquivo de video e recebe uma stream do video

- POST /api/Conteudo
> Recebe nome e arquivos para serem armazenados na api
Recebe:
Titulo
string
	
Send empty value
Tipo
string
	
Send empty value
File
string($binary)
	
Send empty value
Thumbnail
string($binary)
	
Send empty value

- PUT /api/Conteudo/{id}
> recebe nome e arquivos para serem atualizados
Titulo
string
	
Send empty value
Tipo
string
	
Send empty value
File
string($binary)
	
Send empty value
Thumbnail
string($binary)
	
Send empty value

- Delete /api/Conteudo/{id}
> deleta o conteudo

## Paginas

### Pagina inicial

A pagina inicial deve mostrar uma lista com os videos do usuario, ao lado de cada um, deve se ter os botões atualizar e deletar, na parte de cima da lista, deve ser ter uma barra, com o nome do app StreamingSTUDIO, um botão adicionar no meio e o botão Conta na direita

### Pagina de Postar/ atualizar

Considerando que as duas tem design parecidos, as duas devem ter um formulario preenchendo nome, tipo, seletores para a thumbnail e para o video, com o botão abaixo escrito postar/ atualizar

### Pagina de Conta

A pagina deve mostrar as informações do usuario se o mesmo estiver logado, se não estiver, deve mostrar os botões login e registrar

### Login/ registrar

Ambas devem ter um formulario com suas devidas informações necessarias, e o botão login/ registrar embaixo para enviar a requisição