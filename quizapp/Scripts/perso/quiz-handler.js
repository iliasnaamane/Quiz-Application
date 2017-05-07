$(document).ready(function (){
    //get list of divs
    var elements = $("#quiz .element-animation1").toArray();

    //randomly print them back out.
    while (elements.length > 0) {
        var idx = Math.floor((Math.random() * (elements.length - 1)));
        var element = elements.splice(idx, 1);
        $('#quiz').append(element[0]);
    }
    var loading = $('#loadbar').hide();
    $(document)
    .ajaxStart(function () {
        loading.show();
    }).ajaxStop(function () {
    	loading.hide();
    });
    
    $(document).on('click','label.btn' ,function () {
       
        
        
        var newScore = parseInt($('#score').val()) + parseInt($(this).find('input:radio').val()); 
        $('#score').val(newScore);
        $('#step').val(function (i, oldVal) {
            return ++oldVal;
        });
        var data = $('#formQuiz').serialize();
        console.log(JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: "/Quiz/getQuestion",
            data: data,
            success: function (d) {
                $('#quiz').empty();
                $('#quiz').append(d);
                $('h3').html($('#question').val());
                var elements = $("#quiz .element-animation1").toArray();

                //randomly print them back out.
                while (elements.length > 0) {
                    var idx = Math.floor((Math.random() * (elements.length - 1)));
                    var element = elements.splice(idx, 1);
                    $('#quiz').append(element[0]);
                }
            },
            error: function (textStatus, errorThrown) {
                var markExact = ($('#score').val() * 20) / $('#step').val();
                var mark = markExact.toFixed(2);
                
                if (markExact >= 18) {
                    $('#mark-success').html(mark + '/ 20');
                    $('#modal-success').modal('show');
                }
                else if (markExact > 12   && markExact < 18) {
                    $('#mark-info').html(mark + '/ 20');
                    $('#modal-info').modal('show');
                }

                else if (markExact > 8  && markExact < 12) {
                   
                    $('#mark-warning').html(mark + '/ 20');
                    $('#modal-warning').modal('show');
                }
                   
                else {
                   
                    $('#mark-danger').html(mark + '/ 20');
                    $('#modal-danger').modal('show');
                }
                  
                
                
            }
        });
    });
    $(".modal").each(function () {
        $(this).on("hide.bs.modal", function () {
            // put your default event here
            window.location.href = "../../../";
        });
       
    });
    $ans = 3;
});	
