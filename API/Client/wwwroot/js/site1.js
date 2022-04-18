//DataTable
$(document).ready(function () {
    var table = $('.tablemasterdata').DataTable({
        dom: 
            "<'row'<'col-md-3'l><'col-md-5'B><'col-md-4'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7'p>>",
        buttons: [
            'copy', 'excel', 'pdf', {
                text: 'Register',
                action: function (e, dt, node, config) {
                    $('#insertModal').modal()
                }
            }
        ],
        "filter": true,
        "orderMulti": false,
        "ajax": {
            "url": "https://localhost:44315/api/Employees/MasterData",
            "datatype": "json",
            "dataSrc": "result",
        },
        "columns": [
            {
                "data": "nik"
            },
            {
                "data": "fullName"
            },
            {
                "data": "gender"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "+62" + row["phone"];
                },
                "autoWidth": true
            },
            {
                "data": "email"
            },
            {
                "data": "universityName"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "Rp. " + row["salary"];
                },
                "autoWidth": true
            },
            {
                "className": "dt-center", "targets": "_all",
                "orderable": false,
                "data": null,
                "render": function (data, type, row) {
                    return `<button class="bi bi-trash-fill btn-secondary btn-sm" onclick='deletedata("${row["nik"]}")'></button>
                            <button class='bi bi-pencil-square btn-secondary btn-sm' data-toggle="modal" onclick='GetDataUpdate("${row["nik"]}")' data-target='#UpdateModal'></button>`;
                }
            },
        ]
    });
    
    /*table.buttons().disable();*/
    table.buttons().container()
        .appendTo($('.col-sm-6:eq(0)', table.table().container()));
});

$(document).ready(function () {
    const univ = (function () {
        var data = null

        $.ajax({
            async: false,
            type: "GET",
            url: "https://localhost:44315/api/Universities",
            data: {},
            success: function (response) {
                data = response
            },
        })
        return data
    })()

    $.ajax({
        type: "GET",
        url: "https://localhost:44315/api/Employees/MasterData",
        data: {},
    }).done((result) => {
        chartUniversity(result.result)
    }).fail((e) => {
        console.error(e)
    })

    const chartUniversity = (Employees) => {
        const chartUniversity = univ.map((u) => {
            let count = 0

            if (Employees !== undefined) {
                Employees.forEach((emp) => {
                    if (u.name === emp.universityName) {
                        count +=1
                    }
                })
            }
            return { name : u.name, empNum: count}
        })
        
        var options = {
            series: [{
                data: chartUniversity.map((University) => University.empNum),
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    borderRadius: 4,
                    horizontal: true,
                }
            },
            dataLabels: {
                enabled: false
            },
            xaxis: {
                categories: chartUniversity.map((University) => University.name),
            }
        };

        var chart = new ApexCharts(document.querySelector("#univ"), options);
        chart.render();
    }
})

function GetDataUpdate(nik) {
    $.ajax({
        url: "https://localhost:44315/api/Employees/" + nik,
        success: function (results) {
            let result = results.result
            $("#NIKUpdate").attr("value", `${result.nik}`)
            $("#FirstNameUpdate").attr("value", `${result.firstName}`)
            $("#LastNameUpdate").attr("value", `${result.lastName}`)
            $("#PhoneUpdate").attr("value", `${result.phone}`)
            $("#BirthDateUpdate").attr("value", `${result.birthDate}`.toString().substring(0, 10))
            $("#SalaryUpdate").attr("value", `${result.salary}`)
            $("#EmailUpdate").attr("value", `${result.email}`)
            if (result.gender == 0) {
                $('#GenderUpdate').val("0")
            } else
                $('#GenderUpdate').val("1")
        }
    })
}

function Update() {
    event.preventDefault();
    var obj1 = new Object();
    obj1.NIK = $("#NIKUpdate").val()
    obj1.FirstName = $("#FirstNameUpdate").val()
    obj1.LastName = $("#LastNameUpdate").val()
    obj1.Phone = $("#PhoneUpdate").val()
    obj1.BirthDate = $("#BirthDateUpdate").val()
    obj1.Salary = $("#SalaryUpdate").val()
    obj1.Email = $("#EmailUpdate").val()
    obj1.Gender = $("#GenderUpdate").val()
    console.log(obj1)

    $.ajax({
        url: "https://localhost:44315/api/Employees",
        type: "PUT",
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json',
        data: JSON.stringify(obj1)
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'SUCCESS',
        })
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    })
}

function deletedata(nik) {
    Swal.fire({
        title: 'Do you want to delete data?',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Delete',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: "https://localhost:44315/api/Employees/" + nik
            }).done((result) => {
                console.log(result);
            }).fail((error) => {
                console.log(error);
            })
            Swal.fire('Success Delete Data!', '', 'success')
        }
    })
}

function Insert() {
    event.preventDefault();
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.PhoneNumber = $("#phoneNumber").val();
    obj.Birthdate = $("#birthDate").val();
    obj.Salary = $("#salary").val();
    obj.Email = $("#email").val();
    obj.Gender = $("#gender").val();
    obj.Password = $("#password").val();
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val();
    obj.UniversityId = $("#university").val();
    console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:44315/api/Accounts/Register",
        type: "POST",
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json',
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'SUCCESS',
        })
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    })
}

function Login() {
    event.preventDefault();
    var obj = new Object();
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    $.ajax({
        url: "Login/Login",
        type: "POST",
        data: obj
        
    }).done((result) => {
        console.log(result);
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'SUCCESS',
                text: result.message
            }).then(function () {
                window.location.href="Latihan"
            })
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: result.message
            })
        }
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    })
}

function Logout() {
    Swal.fire({
        title: 'Do you want to logout??',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'LogOut',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            location.replace("https://localhost:44307/Login")
        }
    })
}

$.ajax({
    url: "https://localhost:44315/api/Employees/MasterData",
    success: function (result) {
        console.log(result);

        var sumFemale = 0;
        var sumMale = 0;
        for (var i = 0; i < result.result.length; i++) {
            var cowo = result.result[i].gender;

            if (cowo == "Male") {
                sumMale += 1;
            }
            else
                sumFemale += 1;
        }

        var options = {
            series: [sumMale, sumFemale],
            chart: {
                width: 380,
                type: 'pie',
            },
            labels: ['Male', 'Female'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }
})



