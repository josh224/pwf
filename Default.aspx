<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <title>Project Work Flow</title>
        <meta charset="utf-8">
        <meta http-equiv="cache-control" content="max-age=0"/>
        <meta http-equiv="cache-control" content="no-cache"/>
        <meta http-equiv="expires" content="00"/>
        <meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT"/>
        <meta http-equiv="pragma" content="no-cache"/>
        <link href="css/reset.css" rel="stylesheet" type="text/css">
        <link href="css/bootstrap-combined.min.css" rel="stylesheet" type="text/css">
        <link href="css/common.css" rel="stylesheet" type="text/css">
        <link href="css/form.css" rel="stylesheet" type="text/css">
        <link href="css/standard.css" rel="stylesheet" type="text/css">
        <link href="css/960.gs.fluid.css" rel="stylesheet" type="text/css">
        <link href="css/simple-lists.css" rel="stylesheet" type="text/css">
        <link href="css/block-lists.css" rel="stylesheet" type="text/css">
        <link href="css/planning.css" rel="stylesheet" type="text/css">
        <link href="css/table.css" rel="stylesheet" type="text/css">
        <link href="css/calendars.css" rel="stylesheet" type="text/css">
        <link href="js/grid/simple-grid.css" rel="stylesheet"/>
        <link href="js/grid/dialogs.min.css" rel="stylesheet"/>
        <link href="css/wizard.css" rel="stylesheet" type="text/css">
        <style type="text/css">
            .ng-modal-overlay
            {
                /* A dark translucent div that covers the whole screen */
                position: absolute;
                z-index: 9999;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-color: #000000;
                opacity: 0.8;
            }
            .ng-modal-dialog
            {
                /* A centered div above the overlay with a box shadow. */
                z-index: 10000;
                position: absolute;
                width: 50%; /* Default */
                /* Center the dialog */
                top: 50%;
                left: 50%;
                -ms-transform: translate(-50%, -50%);
                -o-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
                -webkit-transform: translate(-50%, -50%);
                -moz-transform: translate(-50%, -50%);
                background-color: #fff;
                box-shadow: 4px 4px 80px #000;
            }
            .ng-modal-dialog-content
            {
                padding: 10px;
                text-align: left;
            }
            .ng-modal-close
            {
                position: absolute;
                top: 3px;
                right: 5px;
                padding: 5px;
                cursor: pointer;
                font-size: 120%;
                display: inline-block;
                font-weight: bold;
                font-family: 'arial', 'sans-serif';
            }
            .hlRed
            {
                color: red;
                font-weight: bold;
            }
            .hlNormal
            {
            }
            .enter-fade
            {
                -webkit-transition: 1s linear opacity;
                -moz-transition: 1s linear opacity;
                -o-transition: 1s linear opacity;
                transition: 1s linear opacity;
                opacity: 0;
            }
            
            .enter-fade.enter-fade-active
            {
                opacity: 1;
            }
            
            .stage
            {
                display: block;
                border: 1px solid orange;
                width: 100%;
            }
            
            .wave-enter, .wave-leave
            {
                -webkit-transition: all 5s;
                -moz-transition: all 5s;
                -o-transition: all 5s;
                transition: all 5s;
            }
            
            .wave-enter
            {
                position: absolute;
                left: 100%;
            }
            
            .wave-enter-active
            {
                left: 0;
            }
            
            .wave-leave
            {
                position: absolute;
                left: 0;
            }

            .wave-leave-active
            {
                left: -100%;
            }
            
            .tooltip-inner
            {
                background-color: cornflowerblue;
                color: #fff;
            }
            
            .tooltip.top
            {
                border-top-color: cornflowerblue;
            }
            
            .tooltip-arrow
            {
                border-top-color: cornflowerblue;
            }
            
            .modal
            {
                position: absolute;
            }
            .animate-show
            {
                -webkit-transition: all linear 0.5s;
                transition: all linear 0.5s;
                line-height: 20px;
                opacity: 1;
                padding: 10px;
                border: 1px solid black;
                background: white;
            }
        
            .animate-show.ng-hide-add,
            .animate-show.ng-hide-remove
            {
                display: block!important;
            }
        
            .animate-show.ng-hide
            {
                line-height: 0;
                opacity: 0;
                padding: 0 10px;
            }

            .check-element
            {
                padding: 10px;
                border: 1px solid black;
                background: white;
            }
            </style>
        <link href="styles/kendo.common.min.css" rel="stylesheet">
        <link href="lib/kendo/styles/kendo.blueopal.css" rel="stylesheet">
        <link href="lib/kendo/dataViz/styles/kendo.dataviz.blueopal.css" rel="stylesheet" />
        <script src="lib/others/jquery-2.1.0.min.js"></script>
        <script src="lib/others/jquery-migrate-1.2.1.min.js"></script>
        <!-- <script type="text/javascript" src="js/html5.js"></script>
        <script src="js/jquery-ui.js"></script>   -->
        
        <script>window.scrollTo = function () {
        };</script>
        
        <script src="js/jquery.csv-0.71.min.js"></script>
        <script src="lib/buckets.jj.js"></script>
                          <!--   quaranteen  '''''''####################'''''












                              End of Quaranteen'''''''''########################################'-->
        <script src="lib/angular_plus_extras/angular.js"></script>
        <script src="lib/angular_plus_extras/angular-route.js"></script>
        <script src="lib/angular_plus_extras/angular-animate.js"></script>
        <script src="lib/others/angular-ui-router.min.js"></script>
        <script src="lib/angular_plus_extras/angular-sanitize.js"></script>
        <script src="lib/kendo/kendo.all.js"></script>
        
        <script src="lib/kendo/dataViz/js/kendo.dataviz.gauge.js"></script>
        <script src="lib/kendo/angular-kendo.js"></script>
        <script src="lib/ui-bootstrap-tpls-0.6.0.js"></script>
            
        <script src="js/grid/simple-grid.js"></script>
        <script src="js/grid/dialogs.min.js"></script>
            
        <!-- <script type="text/javascript" src="js/searchField.js"></script>
        <script type="text/javascript" src="js/common.js"></script>
        <script type="text/javascript" src="js/standard.js"></script>
        <script type="text/javascript" src="js/jquery.tip.js"></script>
        <script type="text/javascript" src="js/jquery.hashchange.js"></script>
        <script type="text/javascript" src="js/jquery.contextMenu.js"></script>
        <script type="text/javascript" src="js/list.js"></script>-->
        <script src="localStorageModule.js"></script>
        <script src="lib/jquery.expander.min.js"></script>
        <!--<link rel="stylesheet" href="pqgrid.min.css" />
        <script type="text/javascript" src="pqgrid.min.js"></script>-->
    </head>
    <body ng-app="myApp">
        <div ng-controller="initController">
            <!-- The template uses conditional comments to add wrappers div for ie8 and ie7 - just add .ie or .ie7 prefix to your css selectors when needed -->
            <!--[if lt IE 9]>
            <div class="ie">
            <![endif]-->
            <!--[if lt IE 8]>
            <div class="ie7">
            <![endif]-->
            <!-- Header -->
            <!-- Status bar -->
            <script></script>
            <div id="status-bar">
                <div class="container_12" id="top">
                    <ul id="status-infos">
                        <li class="spaced">
                            Logged in as: <strong><%=userName%></strong>
                        </li>
                        <li ng-show="false">
                            <a href="#" class="button" title="5 messages">
                                <img src="images/icons/fugue/mail.png" width="16" height="16">
                                <strong>5</strong>
                            </a>
                            <div id="messages-list" class="result-block">
                                <span class="arrow"><span></span></span>
                                        
                                <ul class="small-files-list icon-mail">
                                    <li>
                                        <a href="#">
                                            <strong>10:15</strong> Please update...<br>
                                            <small>From: System</small>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <strong>Yest.</strong> Hi<br>
                                            <small>From: Jane</small>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <strong>Yest.</strong> System update<br>
                                            <small>From: System</small>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <strong>2 days</strong> Database backup<br>
                                            <small>From: System</small>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <strong>2 days</strong> Re: bug report<br>
                                            <small>From: Max</small>
                                        </a>
                                    </li>
                                </ul>
                        
                                <p id="messages-info" class="result-info">
                                    <a href="#">Go to inbox &raquo;</a>
                                </p>
                            </div>
                        </li>
            
                    </ul>
                    <ul id="breadcrumb">
                        <li>
                            <a ng-href="">Hoare Lea</a>
                        </li>
                        <li ng-show="navigation.showPnps">
                            <a ng-href="#" title="PNPs" ng-click="navigate('home');">PNPs</a>
                        </li>
                        <li ng-show="navigation.showProject">
                            <a href="#" ng-click="navigate('SelectedProjects');" title="navigation.projNumber" ng-show="navigation.showProject">{{navigation.projNumber}}</a>
                        </li>
                        <li ng-show="navigation.showQnA">
                            <a href="#" ng-click="navigate('questions');" title="Q &amp; A">Q &amp; A</a>
                        </li>
                        <li ng-show="navigation.showDocs">
                            <a href="#" ng-click="navigate('documents');" title="Home">Documents</a>
                        </li>
                
                    </ul>
                </div>
            </div>
            <!-- End status bar  -->
                
            <div id="header-shadow"></div>
            <!-- End header -->
            <!-- Content -->
            <article class="container_12">
                
                <!-- Content -->
                <div>
                
                    <!--############################ng-animate="animation"###################### ######################topFrame#########################-->
                    <div ui-view="topFrame"  ng-cloak></div>
                    <!--################################################## #################middleFrame###########################-->
                    <div ui-view="middleFrame"  ng-cloak></div>
                    <!--################################################## #################bottomFrame###########################-->
                    <div ui-view="bottomFrame"  ng-cloak></div>
                
                </div>
            </article>
                
            <script type="text/ng-template" id="risk.html">
                <div  >
                <div class="clear"></div>
                <section class="grid_12">
                <div class="block-border" ><form class="block-content form" id="simple_form" action="">
                <h1>Risk Register</h1>
                <p style="font-weight:bold;">{{question.text}}<br/><span ng-show="question.showHelp" style="color:cadetblue; font-size:smaller;font-style:italic;" >
                ({{question.help}})
                </span></p>
                <fieldset>
                <legend>Elemental Ref</legend>
                <table style="width:100%">
                <tr>
                
                <td style="width:20%">
                
                <span style="white-space:nowrap"><label style="white-space:nowrap; display: inline">CAWS Code&nbsp;</label><a style="text-decoration:none;">
                <span> <img id="tipCawsInfo" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a> </span>
                
                </td>
                <td style="width:80%"><input kendo-auto-complete k-options="CawsOptions" name="caws" id="caws" ng-change="cawsChanged()" ng-model="answer.register.caws"  style="width:100%; display: inline-block;"/></td>
                </tr>
                <tr style="padding-top:1em;">
                <td style="white-space:nowrap;width:20%"><label style="white-space:nowrap; display: inline">Description&nbsp;</label>
                
                <a style="text-decoration:none;">
                <span> <img id="tipCawsDescription" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a>
                </td>
                <td style="width:80%; display: inline-block;"><input kendo-auto-complete k-options="CawsDescOptions" name="cawsDescription" id="cawsDescription"  ng-model="answer.register.cawsDescription" style="width:100%"/></td>
                </tr>
                </table>
                
                </fieldset>
                <fieldset>
                <legend>Risk</legend>
                <table style="width:100%;">
                <tr>
                <td style="width:20%"><label style="display: inline">Hazard&nbsp;</label>
                <a style="text-decoration:none;">
                <span> <img id="tipHazard" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a></td>
                <td style="width:80%"><textarea class="full-width" ng-model="answer.register.hazard" name="hazard"  rows="4">{{answer.register.hazard}}</textarea><br/></td>
                </tr>
                <tr>
                <td style="width:20%"><label style="display: inline">Risk&nbsp;</label><a style="text-decoration:none;">
                <span> <img id="tipRisk" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a></td>
                <td style="width:80%; display: inline-block;"><select name="risk" ng-model="answer.register.risk" class="full-width">
                <option>QM</option>
                <option>OHS</option>
                <option>CDM</option>
                <option>EM</option>
                <option>Other</option>
                </select>
                </td>
                </tr>
                <tr>
                <td style="width:20%"><label style="display: inline">Owner&nbsp;</label><a style="text-decoration:none;">
                <span> <img id="tipOwner" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a></td>
                <td style="width:80%"><input kendo-auto-complete k-options="employeeNameOptions" name="Owner" id="Owner"  ng-model="answer.register.owner"   style="width:100%"/></br></td>
                </tr>
                <tr>
                <td style="width:20%"><label style="display: inline">Action&nbsp;</label><a style="text-decoration:none;">
                <span> <img id="tipAction" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a></td>
                <td style="width:80%"><textarea class="full-width" ng-model="answer.register.action" name="action"  rows="4">{{answer.register.action}}</textarea></td>
                </tr>
                <tr>
                <td style="width:20%"><label style="display: inline">Internal</label><a style="text-decoration:none; te ">
                <span> <img id="tipInternal" src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"> </span>
                </a></td>
                <td style="width:80%"><input type="checkbox" ng-model="answer.register.internal">
                </td>
                </tr>
                </table>`
                    
                </fieldset>
                <button class="btn btn-primary" ng-click="ok()">OK</button>
                <button class="btn btn-warning" ng-click="cancel()">Cancel</button>
                </form></div>
                </section>
                </div>
            </script>
            <div>
                <script type="text/ng-template" id="risk9999999.html">
                    <div ng-controller="modalController">
                    <form class="form-horizontal" ng-submit="submit()">
                    <fieldset style="padding-left: 3em;">
                    <legend><h3>Risk Register</h3>{{question.text}}</legend>
                    <!-- Prepended text-->
                    <div class="control-group">
                    <label class="control-label" for="register.caws">CAW0S  code</label>
                    <div class="controls">
                    <div class="input-prepend">
                    <span class="add-on">CAWS</span>
                    <input id="register.caws" name="register.caws" ng-model="answer.caws" class="input-xlarge" placeholder="CAWS code goes here" type="text" required=""><a style="text-decoration:none;"><span class="tooltip" tooltip="insert the CAWS code of the element here; select from Appendix 8.2 of the CDM Handbook e.g. V11"><img src="images/information-blue.png" width="16" height="16" style="vertical-align: text-bottom" class="fl3oat-right"></span></a>
                    </div>
                    </div>
                    </div>
                    <div class="control-group">
                    <label class="control-label" for="textarea">Text Area</label>
                    <div class="controls">
                    <textarea id="textarea" name="textarea">default text</textarea>
                    </div>
                    </div>
                    <div class="control-group">
                    <label class="control-label" for="register.hazard">Hazard description</label>
                    <div class="controls">
                    <textarea id="register.hazard" required="" ng-model="answer.hazard" name="register.hazard" placeholder="give a detailed description of the  risk involved"></textarea>
                    </div>
                    </div>
                    <div class="control-group">
                    <label class="control-label" for="register.projectRisk">Risk</label>
                    <div class="controls">
                    <select id="register.projectRisk" name="register.projectRisk" ng-model="answer.projectRisk" class="input-xlarge">
                    <option>Health and Safety</option>
                    <option>Asbestos</option>
                    </select>
                    </div>
                    </div>
                    <div class="control-group">
                    <label class="control-label" for="register.action">Risk control or action taken</label>
                    <div class="controls">
                    <textarea id="register.action" name="register.action" ng-model="answer.action" placeholder="Be project specific, issue information on residual risk to CDM-C"></textarea>
                    </div>
                    </div>
                    <div class="modal-footer">
                    <button class="btn btn-primary" ng-click="ok()">OK</button>
                    <button class="btn btn-warning" ng-click="cancel()">Cancel</button>
                    </div>
                    </fieldset>
                    </form>
                    </div>
                </script>
            </div>
            <footer style="background: #CC3358">
                
                <div class="float-left" ng-show="analysis.show">
                    <a href="#" ng-show="false" ng-class="analysis.style">Score</a>
                    <a href="#" ng-class="analysis.style">
                        <span ng-class="analysis.style">{{ analysis.score | number:0 }}% Risk = {{analysis.risk}}</span>
                    </a>
                </div>
                <%--<div class="float-right">
                <a ng-href="#top" class="button" ng-click="gototop()">
                <img src="images/icons/fugue/navigation-090.png" width="16" height="16">Page top
                </a>    <ul id="breadcrumb">
                <li>
                <a href="#">Hoare Lea</a>
                </li>
                <li ng-show="navigation.showPnps">
                <a ng-href="" title="PNPs" ng-click="navigate('home');">PNPs</a>
                </li>
                <li ng-show="navigation.showProject">
                <a href="#" ng-click="navigate('SelectedProjects');" title="navigation.projNumber" ng-show="navigation.showProject">{{navigation.projNumber}}</a>
                </li>
                <li ng-show="navigation.showQnA">
                <a href="#" ng-click="navigate('questions');" title="Q &amp; A">Q &amp; A</a>
                </li>
                <li ng-show="navigation.showDocs">
                <a href="#" ng-click="navigate('documents');" title="Home">Documents</a>
                </li>
            
                </ul>
                </div>--%>
                <div class="float-right">
            
                    <a ng-show="navigation.showPnps" ng-href="" class="button" ng-class="{hlRed :state==='home'}" title="PNPs" ng-click="navigate('home');">PNPs</a>
            
                    <a ng-show="navigation.showProject" ng-href="" class="button" ng-class="{hlRed :state==='SelectedProjects'}"  ng-click="navigate('SelectedProjects');" title="navigation.projNumber" ng-show="navigation.showProject">{{navigation.projNumber}}</a>
            
                    <a ng-show="navigation.navigation.showDocs" ng-href="" class="button" ng-class="{hlRed :state==='register'}"  ng-click="navigate('register');" title="Risk Register">Risk Register</a>
            
                    <a ng-show="navigation.showQnA" ng-href="" class="button" ng-class="{hlRed :state==='questions'}"  ng-click="navigate('questions');" title="Q &amp; A">Q &amp; A</a>
            
                    <a ng-show="navigation.showDocs" ng-href="" class="button" ng-class="{hlRed :state==='documents'}"  ng-click="navigate('documents');" title="Home">Documents</a>
            
                    <a ng-href="" class="button" ng-click="gotoHash('top')">
                        <img src="images/icons/fugue/navigation-090.png" width="16" height="16">Page top
                    </a>
                </div>
            </footer>
            <script src="myjs/app.js"></script>
            <script src="js/Smart-Table.min.js"></script>
            <script src="myjs/services.js"></script>
            <script src="myjs/SingleController.js"></script>
            <script src="myjs/controllers.js"></script>
            <script src="myjs/controllers/whereTodayController.js"></script>
            <script src="myjs/controllers/introductionController.js"></script>
            <script src="myjs/controllers/projectsController.js"></script>
            <script src="myjs/controllers/SelectedProjectsController.js"></script>
            <script src="myjs/controllers/ActivityController.js"></script>
            <script src="myjs/controllers/riskRegisterController.js"></script>
            <script src="myjs/controllers/documentsController.js"></script>
            <script src="myjs/controllers/ProjectActivityController.js"></script>
            <script src="myjs/filters.js"></script>
            <script src="myjs/directives.js"></script>
                        
            <script>
                function initController($location, $filter, $anchorScroll, employeeService, questionsService, saveUserService, rolesService, projectsService, serverService, getUserService, streamRiskRegisterToUserService, $scope, $rootScope, $log, $state, $stateParams, localStorageService) {
                    //set some base vars and get data from the server
                    $scope.gotoHash = function(anchorName) {
                        $location.hash(anchorName);
                        console.log(anchorName || json);
                        $anchorScroll();
                    };
                    GetServerData();
                        
                    $scope.getData = function () {
                        GetServerData();
                    };
                    function GetServerData() {
                        $scope.data = $scope.data || {};
                        // now call the projecrs services for this page -.
                        projectsService.async().then(function (d) {
                            $scope.data.projects = d;
                        });
                        // now call the test services for this page -.
                        serverService.async().then(function (d) {
                            $scope.data.test = d;
                        });
                        // now call the roles services for this page -.
                        //rolesService.async().then(function (d) {
                        //	$scope.data.roles = d.d;
                        //});
                        // now call the roles services for this page -.
                        //streamRiskRegisterToUserService.async().then(function (d) {
                        //  $scope.data.riskRegister = d.d;
                        //});
                        // Call the async method and then do stuff with what is returned inside our own then function
                        employeeService.async().then(function (d) {
                            $scope.data.employees = d;
                        });
                        // now call the other services for this page -.
                        questionsService.async().then(function (d) {
                            $scope.data.questions = d;
                        });
                        //$rootScope.selectedProject = fSelectedProject;
                    }//end of getserverdata
                    $scope.data.roles = $scope.data.roles || ["Managing Partner", "Project Partner", "Team Leader", "Project Leader", "Project Review", "Electrical Designer", "Mechanical Designer", "Other"];
                    //declare analysis here so available to all children - used for running score etc.
                    $scope.analysis = {};
                    $scope.analysis.show = false;
                    $scope.analysis.score = 0;
                    $scope.analysis.offset = 0;
                    $scope.analysis.risk = 'low';
                    $scope.analysis.style = 'btn btn-info';
                    //$log.warn("p1op" + JSON.stringify($scope.analysis));
                        
                    ///watch for analysis change
                    $scope.$watch('analysis.score', function () {
                        if ($scope.analysis.score >= 71)  {
                            $scope.analysis.style = "btn btn-danger red";
                            $scope.analysis.risk = "high";
                            if ($scope.analysis.score > 100) {
                                $scope.analysis.score = 100;
                            }
                        }
                        else if ($scope.analysis.score >= 21) {
                            $scope.analysis.style = "btn btn-warning";
                            $scope.analysis.risk = "normal";
                            //$log.warn(">=50" + JSON.stringify($scope.score));
                        }
                        else if ($scope.analysis.score > 0) {
                            $scope.analysis.style = "btn btn-primary";
                            $scope.analysis.risk = "low";
                        }
                        else {
                            $scope.analysis.style = "btn btn-info";
                            //$log.warn(">0>0" + JSON.stringify($scope.score));
                        }
                        return;
                    });
                    //set up the user data
                    $scope.user = {};
                    $scope.user.id = "<%=userId%>";
                    $scope.user.name = "<%=userName%>";
                    $scope.user.email = "<%=userEmailAddress%>";
                    $scope.user.showPnpIntro = true;
                    $scope.user.showSelectedProjectIntro = true;
                    $scope.user.showQuestionsIntro = true;
                    $scope.user.showDocumentsIntro = true;
                    $scope.user.showEmail = true;
                    $scope.user.minimiseActivityLog = false;
                    $scope.user.minimiseGrid = false;
                            
                    try {
                        $scope.user.json = JSON.parse('<%=userJson%>');
                    }
                    catch (e) {
                        $scope.user.json = {};
                    }
                
                    //////////Global Functions////////
                    //localStorage.projectDictionary = undefined
                    $scope.outputLocalStore = function() {
                        //$log.warn("localStorage.projectDictionary" + JSON.stringify(localStorage.projectDictionary));
                    }
                                
                    //1. store user project list on server
                    $scope.user.StoreUserDetailsOnSever = function (projNumber, projName) {
                        try {
                            //$log.warn("V1" + JSON.stringify($scope.user));
                            $scope.ok = true;
                            $scope.user.json = {};
                            $scope.user.json.data = {};
                            $scope.user.json.data.projectNumber = projNumber;
                            $scope.user.json.data.projectName = projName;
                            $scope.user.json.data.projectHistory = $scope.user.json.data.projectHistory || [];
                            if ($scope.user.json.data.projectHistory.indexOf(projNumber) !== -1) {
                                $scope.user.json.data.projectHistory.splice($scope.user.json.data.projectHistory.indexOf(projNumber), 1);
                            }
                            $scope.user.json.data.projectHistory.push(projNumber);
                    
                            if ($scope.user.json.data.projectHistory.length > 5) {
                                $scope.user.json.data.projectHistory.shift();
                            }
                            //call service
                            saveUserService.async($scope.user.id, $scope.user.json).then(function (d) {
                                $scope.ok = d;
                            });
                                    
                            return $scope.ok;
                        }
                        catch (e) {
                            $log.warn("initialController (in Default.aspx) - Global functions $scope.user.StoreUserDetailsOnServer - could not store data! - " + JSON.stringify(e.message));
                            $log.warn(" user " + JSON.stringify($scope.user));
                            return false;
                        }
                    }
                                            
                    //2 local storage
                    $scope.saveToLocalStorage = function () {
                        saveToLocalStorage();
                    }
                    //replaec this after testing is complete - ie ajax call to hl server not local storage
                    function saveToLocalStorage() {
                        if (supportsHtml5Storage()) {
                            localStorage.shortProjectDictionary = localStorage.shortProjectDictionary || {};
                            if ($rootScope.projectDictionary !== undefined) {
                                try {
                                    var dict = {};
                                    angular.forEach($rootScope.projectDictionary.values(), function (value, key) {
                                        //$log.warn("step1" + JSON.stringify(value));
                                        var tmp = {
                                            projectNumber: String(value.projectNumber),
                                            name: value.name,
                                            office: value.office,
                                            date: $filter('date')(value.date),
                                            briefAndReviewStatus:value.briefAndReviewStatus,
                                            riskRegisterStatus: value.riskRegisterStatus,
                                            notes: value.notes,
                                            initialBriefData: {
                                                present1: {
                                                    name: value.initialBriefData.present1.name,
                                                    role: value.initialBriefData.present1.role,
                                                    email: value.initialBriefData.present1.email,
                                                    nexusId: value.initialBriefData.present1.nexusId
                                                },
                                                present2: {
                                                    name: value.initialBriefData.present2.name,
                                                    role: value.initialBriefData.present2.role,
                                                    email: value.initialBriefData.present2.email,
                                                    nexusId: value.initialBriefData.present2.nexusId
                                                },
                                                present3: {
                                                    name: value.initialBriefData.present3.name,
                                                    role: value.initialBriefData.present3.role,
                                                    email: value.initialBriefData.present3.email,
                                                    nexusId: value.initialBriefData.present3.nexusId
                                                },
                                                present4: {
                                                    name: value.initialBriefData.present4.name,
                                                    role: value.initialBriefData.present4.role,
                                                    email: value.initialBriefData.present4.email,
                                                    nexusId: value.initialBriefData.present4.nexusId
                                                },
                                                present5: {
                                                    name: value.initialBriefData.present5.name,
                                                    role: value.initialBriefData.present5.role,
                                                    email: value.initialBriefData.present5.email,
                                                    nexusId: value.initialBriefData.present5.nexusId
                                                },
                                                present6: {
                                                    name: value.initialBriefData.present6.name,
                                                    role: value.initialBriefData.present6.role,
                                                    email: value.initialBriefData.present6.email,
                                                    nexusId: value.initialBriefData.present6.nexusId
                                                },
                                                present7: {
                                                    name: value.initialBriefData.present7.name,
                                                    role: value.initialBriefData.present7.role,
                                                    email: value.initialBriefData.present7.email,
                                                    nexusId: value.initialBriefData.present7.nexusId
                                                },
                                                present8: {
                                                    name: value.initialBriefData.present8.name,
                                                    role: value.initialBriefData.present8.role,
                                                    email: value.initialBriefData.present8.email,
                                                    nexusId: value.initialBriefData.present8.nexusId
                                                },
                                                present9: {
                                                    name: value.initialBriefData.present9.name,
                                                    role: value.initialBriefData.present9.role,
                                                    email: value.initialBriefData.present9.email,
                                                    nexusId: value.initialBriefData.present9.nexusId
                                                },
                                                present10: {
                                                    name: value.initialBriefData.present10.name,
                                                    role: value.initialBriefData.present10.role,
                                                    email: value.initialBriefData.present10.email,
                                                    nexusId: value.initialBriefData.present10.nexusId
                                                },
                                                present11: {
                                                    name: value.initialBriefData.present11.name,
                                                    role: value.initialBriefData.present11.role,
                                                    email: value.initialBriefData.present11.email,
                                                    nexusId: value.initialBriefData.present11.nexusId
                                                },
                                                present12: {
                                                    name: value.initialBriefData.present12.name,
                                                    role: value.initialBriefData.present12.role,
                                                    email: value.initialBriefData.present12.email,
                                                    nexusId: value.initialBriefData.present12.nexusId
                                                }
                                            },
                                                
                                            projectData: {
                                                external1: {
                                                    role: value.projectData.external1.role,
                                                    name: value.projectData.external1.name,
                                                    email:value.projectData.external1.email
                                                },
                                                external2: {
                                                    role: value.projectData.external2.role,
                                                    name: value.projectData.external2.name,
                                                    email: value.projectData.external2.email
                                                },
                                                external3: {
                                                    role: value.projectData.external3.role,
                                                    name: value.projectData.external3.name,
                                                    email: value.projectData.external3.email
                                                },
                                                external4: {
                                                    role: value.projectData.external4.role,
                                                    name: value.projectData.external4.name,
                                                    email: value.projectData.external4.email
                                                },
                                                external5: {
                                                    role: value.projectData.external5.role,
                                                    name: value.projectData.external5.name,
                                                    email: value.projectData.external5.email
                                                },
                                                external6: {
                                                    role: value.projectData.external6.role,
                                                    name: value.projectData.external6.name,
                                                    email: value.projectData.external6.email
                                                },
                                                external7: {
                                                    role: value.projectData.external7.role,
                                                    name: value.projectData.external7.name,
                                                    email: value.projectData.external7.email
                                                },
                                                external8: {
                                                    role: value.projectData.external8.role,
                                                    name: value.projectData.external8.name,
                                                    email: value.projectData.external8.email
                                                },
                                                external9: {
                                                    role: value.projectData.external9.role,
                                                    name: value.projectData.external9.name,
                                                    email: value.projectData.external9.email
                                                },
                                                external10: {
                                                    role: value.projectData.external10.role,
                                                    name: value.projectData.external10.name,
                                                    email: value.projectData.external10.email
                                                }
                                            }
                    
                                        }
                                        dict[value.projectNumber] = tmp;
                                    });
                                    var mystr = [];
                                    angular.forEach(dict, function(value, key) {
                                        mystr.push(value);
                                    })
                                    localStorage.shortProjectDictionary = angular.toJson(mystr);
                                    //$log.warn(" =key and ls=" + JSON.stringify(localStorage.shortProjectDictionary));
                                }
                                catch (e) {
                                    console.log(e.message)
                                }
                                localStorage.projectDictionary = angular.toJson($rootScope.projectDictionary.values());
                                // localStorage.projectDictionary1 = $rootScope.projectDictionary.values();
                            }
                        }
                    };
                    
                    //function saveMinimalToLocalStorage(){
                    //    if (supportsHtml5Storage()) {
                    
                    //        localStorage.projectDictionary = angular.toJson($rootScope.projectDictionary.values());
                    //    }
                    //}
                    //clear storage // only use sparingly
                    //if(supportsHtml5Storage()){
                    //    //localStorage.projectDictionary = undefined;
                    //}
                    function supportsHtml5Storage() {
                        try {
                            return 'localStorage' in window && window['localStorage'] !== null;
                        }
                        catch (e) {
                            return false;
                        }
                    };
    
                    //######################################################################
                    //3. jump to view
                    //######################################################################
                    $scope.navigation = {};
                    $scope.navigation.showPnps = false;
                    $scope.navigation.projNumber = "";
                    $scope.navigation.showProject = false;
                    $scope.navigation.showQnA = false;
                    $scope.navigation.showDocs = false;
                    $scope.navigate = function navigate(to) {
                        //change views
                        $scope.state = $state;
                        $scope.stateParams = $stateParams;
                        $scope.state.go(to);
                    }
                }

                myApp.controller('initController', initController);
            </script>
        </div>
    </body>
</html>
