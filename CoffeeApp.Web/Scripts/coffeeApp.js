function handler(event) {
    var thathtml = $(this).html();
    var that = $(this);
    $(this).animate({ 'width': '+=9em' }, 'fast', function () {
        $(this).find("span").remove();
        var thishtml = $(this).html();
        $(this).html(thishtml + "<button type='reset'><span class='ui-icon ui-icon-close red' style='color: Red'></span></button> <button type='submit'><span class='ui-icon ui-icon-check green'></span></button>");
        $(this).find('button').css('float', 'right');
        $(this).css('background-color', '#FFCC66');
        $(this).find("input[type='number']").css("display", "initial");
        $(this).off();
        $(this).find('button:reset').click(function () {
            that.css('background-color', '#FFFFCC').html(thathtml).animate({ 'width': '-=9em' }, 'fast', function () {
                that.on({ click: handler, mouseenter: function () { that.css('background-color', '#FFCC66'); }, mouseleave: function () { that.css('background-color', '#FFFFCC'); } });
                that.after('<span style="display:none;margin-left:1em; color:Red">Canceled!</span>').next().fadeIn(300).fadeOut(1000, function () { $(this).remove() });
            });
        });
        $(this).submit(function (data) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                error: function (xhr, textStatus, exceptionThrown) {
                    that.after('<span style="display:none;margin-left:1em; color:Red">'+xhr.responseText+'!</span>').next().fadeIn(300).fadeOut(1500, function () { $(this).remove() });
                },
                success: function (result) {
                    $("#chout_msg").empty().html(result);
                    that.after('<span style="display:none;margin-left:1em; color:Green">Added!</span>').next().fadeIn(300).fadeOut(1000, function () { $(this).remove() });
                }               
            });
            $(this).html(thathtml).animate({ 'width': '-=9em' }, 'fast', function () {
                that.css('background-color', '#FFFFCC');
                that.on({ click: handler, mouseenter: function () { that.css('background-color', '#FFCC66'); }, mouseleave: function () { that.css('background-color', '#FFFFCC'); } });
            });
            return false;
        });
    });
}

function deletehandler(event) {
    $(this).submit(function (data) {
        $(this).off();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            datatype: "html",
            cashe: false,
            success: function (result) {
                $("tbody").empty().html(result).find(".delete").on('click', deletehandler);
                updateSummary();
            },
            error: function (xhr, textStatus, exceptionThrown) {
                    $(this).after('<span style="display:none;margin-left:1em; color:Red">Error!</span>').next().fadeIn(300).fadeOut(1000, function () { $(this).remove() });
                }
        });
        event.preventDefault();
        event.stopPropagation();
        return false;
    });
}
function updateSummary() {
    $.ajax({
        url: "/Cart/Summary",
        success: function (result) {
            $("#chout_msg").empty().html(result);
            $('tfoot').find('#total_msg').html("<strong>" + result + "</strong>");
        }
    });
}