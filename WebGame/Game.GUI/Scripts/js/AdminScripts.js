function AdminAjax(id, tabela, user) {
    var data = { tabela: tabela, id: id, user: user};
    var me = $(this);

    $.ajax({
        type: "Post",
        url: 'admin/adminmethod',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ data }),
        dataType: "json",
        success: function (data) {
            location.reload();
        },
    });
}