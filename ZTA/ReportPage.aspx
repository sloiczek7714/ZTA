<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="ZTA.ReportPage" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>ZTA Migration App</title>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <!-- Material Kit CSS -->
    <link href="assets/css/material-dashboard.css" rel="stylesheet" />
</head>
<body class="dark-edition">
    <div class="wrapper ">
        <div class="sidebar" data-color="purple" data-background-color="black" data-image="./assets/img/sidebar-2.jpg">
            <div class="logo">
                <a class="simple-text logo-normal">Menu
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item active  ">
                        <a class="nav-link" href="javascript:void(0)">
                            <i class="material-icons">dashboard</i>
                            <p>Strona główna</p>
                        </a>
                    </li>
                    <li class="nav-item active ">
                        <a class="nav-link" href="./UserPage.aspx">
                            <i class="material-icons">person</i>
                            <p>Profil Uzytkownika</p>
                        </a>
                    </li>
                    <li class="nav-item active ">
                        <a class="nav-link" href="./ListOfProceduresPage.aspx">
                            <i class="material-icons">content_paste</i>
                            <p>Procedura</p>
                        </a>
                    </li>
                    <li class="nav-item active ">
                        <a class="nav-link" href="./ReportPage.aspx">
                            <i class="material-icons">description</i>
                            <p>
                                Raport<p>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="main-panel">
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container-fluid">
                    <div class="navbar-wrapper">
                        <a class="navbar-brand" href="javascript:void(0)">ZTA Migration App</a>
                    </div>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                    </button>
                </div>
            </nav>
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-primary">
                                    <h4 class="card-title ">Podgląd raportu</h4>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div id="piechart" style="width: 900px; height: 500px;"></div>
                                        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                                        <script type="text/javascript">
                                            google.charts.load('current', { 'packages': ['corechart'] });
                                            google.charts.setOnLoadCallback(drawChart);

                                            function drawChart() {

                                                var data = google.visualization.arrayToDataTable([
                                                    ['Task', 'Hours per Day'],
                                                    ['Pozostałe', <%=restActivities  %>],
                                                    ['Ukończone czynności', <%=endedActivities  %>]
                                                ]);

                                                var options = {
                                                    title: 'Postęp',
                                                    backgroundColor: { fill: 'transparent' }
                                                };

                                                var chart = new google.visualization.PieChart(document.getElementById('piechart'));

                                                chart.draw(data, options);
                                            }
                                        </script>




                                        <form runat="server">
                                            <asp:Label ID="systemNamelabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <asp:Label ID="beginDatelabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <asp:Label ID="endDatelabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <br />
                                            <asp:Label ID="bosslabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <br />
                                            <asp:Label ID="employelabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <asp:Label ID="commentlabel" runat="server" CssClass="navbar-brand"></asp:Label>
                                            <br />







                                            <script>

                                                $('#download').click(function () {
                                                    var obj = $(this);
                                                    Utils.disableButton(obj);
                                                    $.get("/MineFace/GetPrintoutFileName")
                                                        .done(function (data) {
                                                            var opt = {
                                                                margin: 1,
                                                                filename: data,
                                                                image: { type: 'jpeg', quality: 1 },
                                                                html2canvas: { scale: 5 },
                                                                jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
                                                            };
                                                            html2pdf().set(opt).from(document.getElementById('element-to-print')).save();
                                                        })
                                                        .fail(function (data) {
                                                            console.log(data);
                                                            Utils.showErrorModal(data);
                                                        }).always(function () {
                                                            Utils.enableButton(obj);
                                                            $('#download-button-spinner').attr('hidden', true);
                                                        });
                                                });</script>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Button ID="generateRaportButton" runat="server" OnClick="pdrCreate" Text="Generuj raport" class="btn btn-primary pull-left" />
                                            <div class="table-responsive">
                                                <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTADBConnectionString %>"></asp:SqlDataSource>
                                                <asp:GridView ID="reportGridView" runat="server" DataSourceID="ZTA" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Numer" DataField="Numer_czynnosci" />
                                                        <asp:BoundField HeaderText="Czynność" DataField="Czynnosc" />
                                                        <asp:BoundField HeaderText="Komenatrz" DataField="Komentarz" />
                                                        <asp:BoundField HeaderText="Data zakończenia" DataField="AData" />
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                        --%>
                    </div>
                    <footer class="footer">
                        <div class="container-fluid">
                            <div class="copyright float-right">
                                &copy;
                        <script>
                            document.write(new Date().getFullYear())            </script>
                                Wojskowa Akademia Techniczna <i class="material-icons">favorite</i> Weronika Buras
                            </div>
                        </div>
                    </footer>
                </div>
            </div>
            <div class="fixed-plugin">
                <div class="dropdown show-dropdown">
                    <a href="#" data-toggle="dropdown">
                        <i class="fa fa-cog fa-2x"></i>
                    </a>
                    <ul class="dropdown-menu">
                        <li class="header-title">Sidebar Filters</li>
                        <li class="adjustments-line">
                            <a href="javascript:void(0)" class="switch-trigger active-color">
                                <div class="badge-colors ml-auto mr-auto">
                                    <span class="badge filter badge-purple active" data-color="purple"></span>
                                    <span class="badge filter badge-azure" data-color="azure"></span>
                                    <span class="badge filter badge-green" data-color="green"></span>
                                    <span class="badge filter badge-warning" data-color="orange"></span>
                                    <span class="badge filter badge-danger" data-color="danger"></span>
                                </div>
                                <div class="clearfix"></div>
                            </a>
                        </li>
                        <li class="header-title">Images</li>
                        <li>
                            <a class="img-holder switch-trigger" href="javascript:void(0)">
                                <img src="../assets/img/sidebar-1.jpg" alt="" />
                            </a>
                        </li>
                        <li class="active">
                            <a class="img-holder switch-trigger" href="javascript:void(0)">
                                <img src="../assets/img/sidebar-2.jpg" alt="" />
                            </a>
                        </li>
                        <li>
                            <a class="img-holder switch-trigger" href="javascript:void(0)">
                                <img src="../assets/img/sidebar-3.jpg" alt="" />
                            </a>
                        </li>
                        <li>
                            <a class="img-holder switch-trigger" href="javascript:void(0)">
                                <img src="../assets/img/sidebar-4.jpg" alt="" />
                            </a>
                        </li>
                    </ul>
                </div>



            </div>

            <script src="./assets/js/core/jquery.min.js"></script>
            <script>alert(google.script.sandbox.mode);</script>
            <script src="./assets/js/core/popper.min.js"></script>
            <script src="./assets/js/core/bootstrap-material-design.min.js"></script>
            <script src="https://unpkg.com/default-passive-events"></script>
            <script src="./assets/js/plugins/perfect-scrollbar.jquery.min.js"></script>
            <script async defer src="https://buttons.github.io/buttons.js"></script>
            <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE"></script>
            <script src="./assets/js/plugins/chartist.min.js"></script>
            <script src="./assets/js/plugins/bootstrap-notify.js"></script>
            <script src="./assets/js/material-dashboard.js?v=2.1.0"></script>
            <script src="./assets/demo/demo.js"></script>
            <script>
                $(document).ready(function () {
                    $().ready(function () {
                        $sidebar = $('.sidebar');

                        $sidebar_img_container = $sidebar.find('.sidebar-background');

                        $full_page = $('.full-page');

                        $sidebar_responsive = $('body > .navbar-collapse');

                        window_width = $(window).width();

                        $('.fixed-plugin a').click(function (event) {
                            // Alex if we click on switch, stop propagation of the event, so the dropdown will not be hide, otherwise we set the  section active
                            if ($(this).hasClass('switch-trigger')) {
                                if (event.stopPropagation) {
                                    event.stopPropagation();
                                } else if (window.event) {
                                    window.event.cancelBubble = true;
                                }
                            }
                        });

                        $('.fixed-plugin .active-color span').click(function () {
                            $full_page_background = $('.full-page-background');

                            $(this).siblings().removeClass('active');
                            $(this).addClass('active');

                            var new_color = $(this).data('color');

                            if ($sidebar.length != 0) {
                                $sidebar.attr('data-color', new_color);
                            }

                            if ($full_page.length != 0) {
                                $full_page.attr('filter-color', new_color);
                            }

                            if ($sidebar_responsive.length != 0) {
                                $sidebar_responsive.attr('data-color', new_color);
                            }
                        });

                        $('.fixed-plugin .background-color .badge').click(function () {
                            $(this).siblings().removeClass('active');
                            $(this).addClass('active');

                            var new_color = $(this).data('background-color');

                            if ($sidebar.length != 0) {
                                $sidebar.attr('data-background-color', new_color);
                            }
                        });

                        $('.fixed-plugin .img-holder').click(function () {
                            $full_page_background = $('.full-page-background');

                            $(this).parent('li').siblings().removeClass('active');
                            $(this).parent('li').addClass('active');


                            var new_image = $(this).find("img").attr('src');

                            if ($sidebar_img_container.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                                $sidebar_img_container.fadeOut('fast', function () {
                                    $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                                    $sidebar_img_container.fadeIn('fast');
                                });
                            }

                            if ($full_page_background.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                                var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                                $full_page_background.fadeOut('fast', function () {
                                    $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
                                    $full_page_background.fadeIn('fast');
                                });
                            }

                            if ($('.switch-sidebar-image input:checked').length == 0) {
                                var new_image = $('.fixed-plugin li.active .img-holder').find("img").attr('src');
                                var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                                $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                                $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
                            }

                            if ($sidebar_responsive.length != 0) {
                                $sidebar_responsive.css('background-image', 'url("' + new_image + '")');
                            }
                        });

                        $('.switch-sidebar-image input').change(function () {
                            $full_page_background = $('.full-page-background');

                            $input = $(this);

                            if ($input.is(':checked')) {
                                if ($sidebar_img_container.length != 0) {
                                    $sidebar_img_container.fadeIn('fast');
                                    $sidebar.attr('data-image', '#');
                                }

                                if ($full_page_background.length != 0) {
                                    $full_page_background.fadeIn('fast');
                                    $full_page.attr('data-image', '#');
                                }

                                background_image = true;
                            } else {
                                if ($sidebar_img_container.length != 0) {
                                    $sidebar.removeAttr('data-image');
                                    $sidebar_img_container.fadeOut('fast');
                                }

                                if ($full_page_background.length != 0) {
                                    $full_page.removeAttr('data-image', '#');
                                    $full_page_background.fadeOut('fast');
                                }

                                background_image = false;
                            }
                        });

                        $('.switch-sidebar-mini input').change(function () {
                            $body = $('body');

                            $input = $(this);

                            if (md.misc.sidebar_mini_active == true) {
                                $('body').removeClass('sidebar-mini');
                                md.misc.sidebar_mini_active = false;

                                $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();

                            } else {

                                $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar('destroy');

                                setTimeout(function () {
                                    $('body').addClass('sidebar-mini');

                                    md.misc.sidebar_mini_active = true;
                                }, 300);
                            }

                            // we simulate the window Resize so the charts will get updated in realtime.
                            var simulateWindowResize = setInterval(function () {
                                window.dispatchEvent(new Event('resize'));
                            }, 180);

                            // we stop the simulation of Window Resize after the animations are completed
                            setTimeout(function () {
                                clearInterval(simulateWindowResize);
                            }, 1000);

                        });
                    });
                });
            </script>
</body>
</html>
