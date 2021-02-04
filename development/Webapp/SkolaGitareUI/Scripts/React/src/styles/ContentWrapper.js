import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import pink from "@material-ui/core/colors/pink";


const useStyles = makeStyles((theme) => ({
    gridRoot: {
        flexGrow: 4,
    },
    root: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.paper,
        display: "flex",
        height: "500px",
    },
    tabs: {
        borderRight: `1px solid ${theme.palette.divider}`,
    },
    paper: {
        padding: theme.spacing(2),
        textAlign: "center",
        color: pink[600],
        fontFamily: "Roboto",
        fontWeight: "bold"
    },
    papperTabs: {},
    button: {
        backgroundColor: pink[500],
        marginTop: "5px",
        color: "white",
    },
    title: {
        letterSpacing: "3px",
    },
    contentGrid: {},
}));

export default useStyles;