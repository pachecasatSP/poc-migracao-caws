(function ($) {
    $.fn.Disable = function () {
        $(this).attr('disabled', 'disabled');
        return this;
    };
    $.fn.Enable = function () {
        $(this).removeAttr('disabled');
        return this;
    };
    $.fn.Clear = function () {
        $(this).val('');
        var objects = $('input[name^="' + $(this).prop('id').replace(/\_/g, '$') + '"]');
        for (var i = 0; i < objects.length; i++) {
            $(objects[i]).removeAttr('checked');
        }
        $(this).prop('selectedIndex', 0);
        return this;
    };
    $.fn.Check = function (index) {
        var objects = $('input[name^="' + $(this).prop('id').replace(/\_/g, '$') + '"]');
        $(objects[index]).attr('checked', 'checked');
        return this;
    };
    $.fn.jqGridDisableAdd = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#add_" + gid).addClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridDisableEdit = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#edit_" + gid).addClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridDisableDel = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#del_" + gid).addClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridEnableAdd = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#add_" + gid).removeClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridEnableEdit = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#edit_" + gid).removeClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridEnableDel = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#del_" + gid).removeClass('ui-state-disabled');
        return this;
    };
    $.fn.jqGridHideAdd = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#add_" + gid).hide();
        return this;
    };
    $.fn.jqGridHideEdit = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#edit_" + gid).hide();
        return this;
    };
    $.fn.jqGridHideDel = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#del_" + gid).hide();
        return this;
    };
    $.fn.jqGridShowAdd = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#add_" + gid).show();
        return this;
    };
    $.fn.jqGridShowEdit = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#edit_" + gid).show();
        return this;
    };
    $.fn.jqGridShowDel = function () {
        var gid = $.jgrid.jqID(this[0].id);
        $("#del_" + gid).show();
        return this;
    };
})(jQuery);