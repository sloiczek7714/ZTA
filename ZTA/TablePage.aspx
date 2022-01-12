<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TablePage.aspx.cs" Inherits="ZTA.TablePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="../assets/img/favicon.png" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Panel logowania</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <link href="assets/css/material-dashboard.css?v=2.1.0" rel="stylesheet" />
    <link href="assets/demo/demo.css" rel="stylesheet" />

</head>
<body class="dark-edition">
    <div class="content">
        <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top " id="navigation-example">
            <div class="container-fluid">
                <div class="navbar-wrapper">
                    <a class="navbar-brand" href="javascript:void(0)">ZTA Migration App</a>
                </div>
            </div>
        </nav> <br /><br /> <br />
        <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-20">
                        <div class="card">
                            <div class="card-header card-header-primary">
                                <h4 class="card-title">Tabela przykładowej interpretacji oceny stanu bezpieczeństwa</h4>
                                <p class="card-category">Źródło: „Bezpieczeństwo Informacyjne Nowe Wyzwania” K. Liderman wydanie II s.220</p>
                            </div>
                            <div class="card-body table-responsive">
                                <table class="table table-hover">
                                    <thead class="text-warning">
                                        <th>Wartość ocen</th>
                                        <th>Interpretacja oceny</th>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>WYSOKI</td>
                                            <td>Jeśli wartości koncepcji wskaźników ryzyka mieszczą się w określonym wcześnej przedziale</td>
                                        </tr>
                                        <tr>
                                            <td>ALERT</td>
                                            <td>Jeśli przynajmniej jeden z kluczowych wskaźników ryzyka znajdzie się poza określonym wcześniej przedziałem i nie osiągnął wartości kwalifikującej go jako incydent oraz istnieją procedury przywracania tej wartości do właściwego przedziału</td>
                                        </tr>
                                        <tr>
                                            <td>INCYDENT</td>
                                            <td>Jeśli przynajmniej jeden z kluczowych wskaźników ryzyka znajdzie się poza określonym wcześniej przedziałem i osiągnął wartości kwalifikującą go jako incydent oraz istnieją procedury przywracania tej wartości do właściwego przedziału</td>
                                        </tr>
                                        <tr>
                                            <td>KRYZYS</td>
                                            <td>Jeśli przynajmniej jednej wartości z kluczowych wskaźników ryzyka nie udało się sprowadzić do właściwego przedziału w wymaganym czasie lub jeśli nie istnieją procedury odpowiadające temu zdarzeniu</td>
                                        </tr>
                                        <tr>
                                            <td>WYMAGANA ANALIZA RYZYKA</td>
                                            <td>Jeśli wartość kluczowej wartości ryzyka zmian organizacyjnych i/lub zmian technicznych przekroczą dopuszczalne przedziały; dopóki nie zostaną zakończona ponowna analiza ryzyka, operacyjny stan bezpieczeństwa ma wartość alert</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="footer">
                <div class="container-fluid">
                    <div class="copyright float-right">
                        &copy;           
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                        Wojskowa Akademia Techniczna <i class="material-icons">desktop_windows</i> Weronika Buras         
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../assets/js/core/jquery.min.js"></script>
    <script src="../assets/js/core/popper.min.js"></script>
    <script src="../assets/js/core/bootstrap-material-design.min.js"></script>
    <script src="https://unpkg.com/default-passive-events"></script>
    <script src="../assets/js/plugins/perfect-scrollbar.jquery.min.js"></script>
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <script src="../assets/js/plugins/chartist.min.js"></script>
    <script src="../assets/js/plugins/bootstrap-notify.js"></script>
    <script src="../assets/js/material-dashboard.js?v=2.1.0"></script>
    <script src="../assets/demo/demo.js"></script>

</body>

</html>













