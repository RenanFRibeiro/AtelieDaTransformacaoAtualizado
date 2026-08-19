# Correção dos eventos do Desktop

## O problema
Os botões de navegação do `MainForm` tinham a aparência configurada no Designer, mas o `Tag` e o evento `Click` dependiam de um método `ConfigureNavButton` que não estava sendo chamado. Por isso os botões apareciam, mas não navegavam.

## Correção
A navegação agora é ligada em `MainForm.cs`, depois do `InitializeComponent()`, sem recriar ou sobrescrever o visual feito no Designer.

Os botões recebem somente a chave funcional:
- Dashboard -> `dashboard`
- Produtos -> `products`
- Categorias -> `categories`
- Usuários -> `users`
- Perfil -> `profile`

O evento de logout também foi movido para `MainForm.cs` para que uma alteração no Designer não remova a lógica.

## Regra para o projeto
O `.Designer.cs` deve cuidar da aparência e dos controles.
Os arquivos `.cs` devem cuidar dos eventos e da lógica.

Assim, você pode alterar cores, tamanhos, posições, textos, fontes e propriedades do Guna2 no Designer sem perder a navegação.
