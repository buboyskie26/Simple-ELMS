

$(function () {

    var errorText = "An error has occurred. An administrator has been notified. Please try again later";
    $("button[name='SaveSelectedUsers']").prop('disabled', true);


    $('select').on('change', function () {

        var url = '/Admin/UsersToCategory/UserListPerCategory?categoryId=' + this.value;

        if (this.value != 0) {
            $.ajax({
                type: 'GET',
                url,
                success: function (data) {


                    $('#UsersPopulate').html(data);
                    $("button[name='SaveSelectedUsers']").prop('disabled', false);

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    PresentClosableBootstrapAlert("#alert_placeholder", "danger", "An error occurred!", errorText);
                    console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);
                }
            });

        } else {
            $("button[name='SaveSelectedUsers']").prop('disabled', true);
            $("input[type=checkbox]").prop("checked", false);
            $("input[type=checkbox]").prop("disabled", true);
        }
    });

    $("#SaveSelectedUsers").click(function () {

        var url = '/Admin/UsersToCategory/SaveSelectedUserFromCategory';
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        var usersSelected = [];

        var categoryId = $("#CategoryId").val();


        $("input[type=checkbox]:checked").each(function () {

            var userModel = {
                userId: $(this).attr("value")
            };

            usersSelected.push(userModel);
        });

        var usersData = {
            CategoryId: categoryId,
            __RequestVerificationToken: antiForgeryToken,
            UsersSelected: usersSelected
        };

        $.ajax({
            type: 'POST',
            data: usersData,
            url,
            success: function (data) {

                $("#UsersPopulate").html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                PresentClosableBootstrapAlert("#alert_placeholder", "danger", "An error occurred!", errorText);
                console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);

            }
        });

    });
});
