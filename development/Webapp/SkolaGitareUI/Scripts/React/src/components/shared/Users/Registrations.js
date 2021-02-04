import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import CheckIcon from '@material-ui/icons/Check';
import request from "../../../request";


const useStyles = makeStyles({
    table: {
        minWidth: 650
    },
    root: {
        width: "100%",
    },
    title: {
        flex: "1 1 100%"
    },
    container: {
        maxHeight: 400,
    },
});



export default function RegistrationList(props) {
    const classes = useStyles();
    const [rows, setRows] = React.useState(null);

    React.useEffect(() => {
        let mounted = true;
        request(props.accessToken, {
            method: "get",
            url: props.url,
        }).then((data) => {
            if (mounted) {
                setRows(data);
            }
        });
        return () => (mounted = false);
    }, []);

    return (
        <Paper className={classes.root}>
            <TableContainer className={classes.container}>
                {rows ?
                    (<div>
                <Toolbar>
                    <Typography
                        className={classes.title}
                        variant="h6"
                        id="registrationTable"
                        component="div"
                    >
                        Zahtjevi za registracijom
        </Typography>
                </Toolbar>
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            <TableCell align="left">Ime</TableCell>
                            <TableCell align="left">Prezime</TableCell>
                            <TableCell align="left">Email</TableCell>
                            <TableCell align="left">Telefon</TableCell>
                            <TableCell align="left"></TableCell>
                            <TableCell align="left"></TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (
                            <TableRow key={row.name}>
                                <TableCell component="th" scope="row">
                                    {row.FirstName}
                                </TableCell>
                                <TableCell align="left">{row.LastName}</TableCell>
                                <TableCell align="left">{row.email}</TableCell>
                                <TableCell align="left">{row.Phone}</TableCell>
                                <TableCell align="right">

                                </TableCell>

                                <TableCell align="right">
                                    <Tooltip title="Potvrdi" aria-label="add">

                                        <IconButton aria-label="delete" color="secondary" size="small" onClick={() => console.log("potvrdi učenika")}>
                                            <CheckIcon />
                                        </IconButton>
                                    </Tooltip>
                                    <Tooltip title="Obriši" aria-label="add">

                                        <IconButton aria-label="delete" color="secondary" size="small" onClick={() => console.log("izbriši zahtjev za regsitraciju")}>
                                            <DeleteIcon />
                                        </IconButton>
                                    </Tooltip>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                        </Table>
                    </div>)
                    : <div>
                        <Toolbar>
                            <Typography
                                stickyHeader
                                variant="h6"
                                id="tableTitle"
                                component="div"
                                className={classes.title}
                            >
                                Nema novih zahtjeva
                            </Typography>
                        </Toolbar>
                    </div>}
            </TableContainer>
        </Paper>
    );
}
