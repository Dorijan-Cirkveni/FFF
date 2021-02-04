import React from "react";
import { Link, BrowserRouter, Switch, Route } from "react-router-dom";

import { AuthConsumer } from "../authContext";

import Button from "@material-ui/core/Button";

import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";

import Logout from "./Logout";
import Login from "./Login";
import ProfileButton from "./shared/ProfileButton";


import HomePage from "../pages/shared/home";
import KalendarPage from "../pages/shared/kalendar";
import GroupLessonsPage from "../pages/shared/groupLessons";
import LessonsPage from "../pages/shared/lessons";
import ReservationsPage from "../pages/shared/reservations";
import UsersPage from "../pages/shared/users";
import TransactionsPage from "../pages/shared/transactions";
import RegistrationsPage from "../pages/shared/registrations";
import ProfilePage from "../pages/shared/profile";


const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
    },
    menuButton: {
        marginRight: theme.spacing(2),
    },
    title: {
        flexGrow: 1,
    },
    toolbar: {
        backgorundColor: "white",
    },
    AppBar: {
        backgroundColor: "#FFF",
    },
}));

export default function Header() {
    const classes = useStyles();
    const [pass, setPass] = React.useState("");
    const [email, setEmail] = React.useState("");
 

   
    return (
        <div className={classes.root}>
            <BrowserRouter>
                <AppBar position="static" className={classes.AppBar}>
                    <Toolbar className={classes.toolbar}>
                        <Typography variant="h6" className={classes.title}>
                            <Link to="/">
                                <Button>Početna</Button>
                            </Link>
                            <Link to="/grouplessons">
                                <Button>Grupne Lekcije</Button>
                            </Link>
                            <Link to="/faq">
                                <Button>FAQ</Button>{" "}
                            </Link>
                        </Typography>
                        <AuthConsumer>
                            {({ authenticated, user }) =>
                                authenticated ? (
                                    <div style={{ display: "flex" }}>
                                        <Logout />
                                        <ProfileButton user={user}/>
                                    </div>
                                ) : (   
                                        <div style={{ display: "flex" }}>
                                            <Login values={{ pass: pass, email: email }}></Login>
                                            <h2>REG</h2>
                                        </div>
                                      
                                       
                                    )
                            }
                        </AuthConsumer>
                    </Toolbar>
                </AppBar>
                <Switch>
                    <Route exact path="/" component={HomePage} />
                    <Route path="/grouplessons" component={GroupLessonsPage} />
                    <Route path="/profil" component={ProfilePage} />
                    <Route path="/kalendar" component={KalendarPage} />
                    <Route path="/lekcije" component={LessonsPage} />
                    <Route path="/rezervacije" component={ReservationsPage} />
                    <Route path="/korisnici" component={UsersPage} />
                    <Route path="/clanarine" component={TransactionsPage} />
                    <Route path="/registracije" component={RegistrationsPage} />

                </Switch>
            </BrowserRouter>
        </div>
    );
}
