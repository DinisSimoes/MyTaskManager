# MyTaskManager

API para fazer a gestão de tarefas/TODOs

# Pontos a ter em conta
Foram implementados os seguintes pontos
 - **CRUD de Tarefas (To-Dos):** A API oferece funcionalidades de criação, leitura, atualização e exclusão (CRUD) para a gestão de tarefas.
 - **Criação de Usuário:** A API permite criar novos usuários
 - **Geração de Token:** Após a criação do usuário, é possível gerar um token de autenticação. Esse token é necessário para acessar os endpoints protegidos da API.
 - **Encriptação de Senha:** A senha do usuário é encriptada para garantir maior segurança.
 - **Verificação de Existência de Usuário:** A API verifica se o usuário já existe antes de realizar a criação ou autenticação.
 - E**xecução em Docker:** A aplicação está configurada para ser executada em contêineres Docker.
 - **Migrations:** As migrations precisam ser aplicadas antes de rodar a aplicação para garantir que o banco de dados (localmente) esteja corretamente configurado.

# Tarefas futuras

O software foi desenvolvido rapidamente, mas algumas melhorias podem ser implementadas no futuro:

 - **Validação de Comprimento de Senha:** Implementar uma validação para exigir senhas com, no mínimo, 12 caracteres, incluindo pelo menos uma letra maiúscula e um número.
 - **Desenvolvimento de Testes Unitários:** Criar mais testes unitários para cobrir pelo menos 90% do código, garantindo maior confiabilidade e manutenção.
 - **Adição de Tags:** Implementar a funcionalidade de tags, semelhante ao que é encontrado no Samsung Notes e no Apple Notes, para melhorar a organização das tarefas.
 - **Tipos de Tarefas/To-Dos:** Expandir a aplicação para suportar diferentes tipos de tarefas, como listas de verificação e tarefas com calendários.
 - **API de Filtro/Pesquisa:** Implementar uma API para permitir filtros e buscas pela descrição das tarefas, ou partes dela. Isso poderia ser feito utilizando expressões regulares ou stored procedures no banco de dados, sendo necessário investigar a melhor abordagem para a pesquisa eficiente.

# Autenticação

A API utiliza JWT para proteger os endpoints. Para acessar os endpoints protegidos, siga os seguintes passos:

1) Registrar um usuário: Utilize o endpoint api/auth/register para criar um novo usuário, passando um username e uma password no corpo da requisição.

2) Gerar o Token de Autenticação: Após criar o usuário, utilize o endpoint api/auth/token para obter o token de autenticação. Na resposta, você receberá o token.

3) Autenticar-se nos Endpoints Protegidos:

 - Copie o token gerado e clique em Authorize na interface da documentação.

![image](https://github.com/user-attachments/assets/4dd8af8e-9fe6-45d5-97fa-9b84fb41e617)

 - Uma janela de autenticação será exibida. No campo Value, cole o token copiado anteriormente.
![image](https://github.com/user-attachments/assets/940b0e1c-a9cd-44d4-a3d6-8e97f231c1cc)

 - Após preencher o campo, a janela será fechada, e o token será adicionado com sucesso. A partir desse momento, todos os endpoints estarão autenticados e prontos para serem usados.
![image](https://github.com/user-attachments/assets/653c86aa-d9ac-4ed1-b586-7a69c44e6746)

4) Logout e Troca de Usuário:

Para fazer logout e realizar login com outro usuário, clique em Logout.
Em seguida, gere um novo token de autenticação, caso o novo usuário já exista.


