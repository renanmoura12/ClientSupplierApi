## API desenvolvida.

Versão 7.0;

Na api desenvolvida precisei usar o Mysql por possuir mais experiência com esse banco para poder implementar
mais rapidamente. como tambem não consegui implementar os testes unitários por conta do prazo.

Foi utilizado o método Code First para a modelagem do banco usando migrations.

<h1 align="center">
	<img src= "https://ik.imagekit.io/rmoura/image.png?updatedAt=1717086608362">
</h1>

## Como rodar o projeto.

dotnet build
dotnet run
abra o swagger e teste: https://localhost:7212/swagger
ou veja online: https://customer-supplier-api-3433ad5e3d94.herokuapp.com/swagger

## Token jwt.

Crie um usuario em "User/register". Em seguida faça login em "User/login".
Após isso autentique no "Authorize" do swagger copiando somente o token.

## Método patch Json.

Foi escolhido esse tipo para alteração parcial das informações, no proprio endpoint encontrará mais informações.