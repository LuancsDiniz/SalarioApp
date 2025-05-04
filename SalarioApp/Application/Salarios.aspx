<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salarios.aspx.cs" Inherits="SalarioApp.Application.Salarios" %>
<!-- Diretiva da página que define linguagem, código-behind e classe herdada -->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>Salários</title></head>
<body>
<form runat="server"> <!-- Formulário ASP.NET Web Forms -->

    <!-- Botão que dispara o cálculo dos salários -->
    <asp:Button ID="btnCalc" runat="server" Text="Calcular" OnClick="Calc_Click" Style="margin-bottom: 5px;" />
    
    <!-- Label usada para exibir mensagens de status ou erro -->
    <asp:Label ID="msg" runat="server" />

    <!-- GridView que exibe os dados de salários calculados -->
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="pessoa_nome" HeaderText="Nome" />
            <asp:BoundField DataField="cargo_nome" HeaderText="Cargo" />
            <asp:BoundField DataField="salario" HeaderText="Salário" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
</form>
</body>
</html>
