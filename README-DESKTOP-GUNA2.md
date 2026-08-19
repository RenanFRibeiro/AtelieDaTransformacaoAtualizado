# Desktop Guna2 — Ateliê da Transformação

O projeto Desktop foi reorganizado para que a interface fique separada da lógica e possa ser alterada visualmente pelo Windows Forms Designer.

## O que foi alterado

- Guna.UI2.WinForms 2.0.4.8 adicionado via NuGet.
- Forms e UserControls agora usam `partial class` + arquivos `*.Designer.cs`.
- `MainForm`, `LoginForm`, `ProductDialog`, `CategoryDialog` e `SimpleUserDialog` possuem Designer.
- `DashboardUserControl`, `ProductsUserControl`, `CategoriesUserControl`, `UsersUserControl` e `ProfileUserControl` possuem Designer.
- A lógica de API/CRUD permanece nos arquivos `.cs` e os controles/layout ficam no `.Designer.cs`.
- Não há dependência de `.resx` para os formulários do Desktop.

## Como editar visualmente

1. Abra a solução no Visual Studio.
2. No Solution Explorer abra `AtelieDaTransformacao.Desktop`.
3. Expanda `Forms` ou `UserControls`.
4. Clique com o botão direito em `MainForm.cs`, `LoginForm.cs`, `ProductsUserControl.cs` etc.
5. Escolha **Exibir Designer**.
6. Use a janela **Propriedades** para alterar tamanho, cores, textos, bordas, fontes, Dock, Anchor e outros valores.

O código de comportamento deve continuar nos arquivos `.cs`. Evite editar manualmente o `*.Designer.cs` quando a alteração puder ser feita pelo Designer.

## Guna2

O pacote é restaurado automaticamente pelo NuGet. Se os controles não aparecerem na Toolbox:

- Build/Rebuild a solução.
- Abra **Exibir > Caixa de Ferramentas**.
- Clique com o botão direito > **Escolher Itens...**.
- Procure pelos controles `Guna2...`.

## Execução

1. Inicie `AtelieDaTransformacao.API`.
2. Confirme a URL configurada em `AtelieDaTransformacao.Desktop/appsettings.json`.
3. Inicie `AtelieDaTransformacao.Desktop`.
4. Faça login com o usuário inicial configurado pelo seed do projeto.

## Importante

O ambiente onde este pacote foi preparado não possui o SDK do .NET instalado, portanto o build final precisa ser validado no Visual Studio da máquina de desenvolvimento.
