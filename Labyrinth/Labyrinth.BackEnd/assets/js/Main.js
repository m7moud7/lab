$(function () {

    // Add New Number 1
    if ($(".ClickInput").length > 0) {
        $(".ClickInput").on("click", function () {
            $('.ClickFun').toggleClass('displayCategory');
        })
    }

    // Add New Number 2
    if ($(".ClickInputPlace").length > 0) {
        $(".ClickInputPlace").on("click", function () {
            $('.ClickFunPlace').toggleClass('displayCategory');
        })
    }

    // Reorder elements in a list or grid using the mouse.
    if ($("#sortable").length > 0) {
        $("#sortable").sortable();
        $("#sortable").disableSelection();
    }

});