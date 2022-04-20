let app = {
    Core: {
        CreateTable: function(id) {
            $("#" + id).DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });           
        },
        GetStudentDetails: function(personId){
            // Método .post() de JQuery
            $.post("/estudiantes/ficha/" + personId, {}, function(data){
                $(".modal-tittle").html("Ficha del estudiante " + personId);
                $("#modalbody").html(data);
                $("#b1").html("Cerrar");
                $("#b2").hide();
                $("#modal").modal("show");
            });
        },
    },

    Data: {},
    
    Page: {
        Estudiantes: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("estudiantes");
                }
            },
            File: {
                OnLoad: function() {
                    App.Core.CreateTable("e");
                    $("button.btn-detalle").click(function(e) {
                        App.Core.GetStudentDetails($(this).data("PersonID"));                        
                    });
                }
            }
        },
    }
}