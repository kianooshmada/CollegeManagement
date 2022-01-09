
function DeleteItemFromList(id) {
    if (confirm("Are you sure want to delete it?")) {
        var $input = $('<input type="hidden" name="itemForDelete" value="' + id + '" />');
        $("#pagedListFilter").append($input).submit();
    }
}

function DeleteListItem(url, id) {
    if (confirm("Are you sure want to delete it?")) {
        var page = getParameterByName('page');
        if (isNaN(page) || parseInt(page) == 0)
            page = 1;
        else
            page = parseInt(page);
        var $form = $('<form method="post" action="' + url + '?page=' + page + '"><input type="hidden" name="page" id="page" value="' + page + '"><input type="hidden" name="itemForDelete" value="' + id + '" /></form>');
        $("#pagedListFilter :input").each(function () {
            $form.append('<input type="hidden" name="' + this.name + '" value="' + this.value + '" />');
        });
        $form.appendTo($("body")).submit().remove();
    }
}