import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Box from "@material-ui/core/Box";
import Collapse from "@material-ui/core/Collapse";
import IconButton from "@material-ui/core/IconButton";

import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableRow from "@material-ui/core/TableRow";
import Typography from "@material-ui/core/Typography";
import KeyboardArrowDownIcon from "@material-ui/icons/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";

import Link from "@material-ui/core/Link";

import DeleteIcon from "@material-ui/icons/Delete";

import EditLesson from "./EditLesson";

import request from "../../../request.js";

const useRowStyles = makeStyles({
    root: {
        "& > *": {
            borderBottom: "unset",
        },
    },
});



function Row(props) {
    const [open, setOpen] = React.useState(false);
    const classes = useRowStyles();
    const handleDelete = () => {
       
    request(props.accessToken, {
            method: "delete",
            url: `Lessons/${props.row.id}`,
            }).then((data) => {
                props.handleDelete();
                })
            

    }
    return (
        <React.Fragment>
            <TableRow className={classes.root}>
                <TableCell component="th" scope="row">
                    {props.row.name}
                </TableCell>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell align="right">
                    <div style={{ display: "inline-flex" }}>
                        <EditLesson lesson={props.row} accessToken={props.accessToken} handleEdit={props.handleDelete} />

                        <IconButton aria-label="delete" color="secondary" size="small" onClick={handleDelete}>
                            <DeleteIcon />
                        </IconButton>
                    </div>
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box margin={1}>
                            
                            <Table size="small" aria-label="purchases">
                                <TableBody>
                                    <TableRow>
                                        <TableCell align="left">
                                            <b>Bilješke:</b>
                                        </TableCell>
                                        <TableCell align="left">{props.row.notes ? props.row.notes : ""}</TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell align="left">
                                            <b>Učitelj:</b>
                                        </TableCell>
                                        <TableCell align="left">{props.row.from.name + " " +props.row.from.surname}</TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell align="left">
                                            <b>Materijali:</b>
                                        </TableCell>
                                        <TableCell align="left">
                                            { props.row.downloadLink ?
                                                <Link
                                                    href={props.row.downloadLink}
                                                    download
                                                    style={{ textDecoration: "none" }}
                                                >
                                                    Pregled
                      </Link> : <h7>Nije dostupno</h7>}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell align="left">
                                            <b>Učenik:</b>
                                        </TableCell>
                                                    <TableCell>
                                                        <Typography variant="h9">
                                                {props.row.for.name + " " + props.row.for.surname}
                                                        </Typography>
                                                    </TableCell>

                                                    <TableCell>
                                                        <Typography variant="h9">
                                                            <b>Telefon:</b>
                                                        </Typography>
                                                    </TableCell>
                                                    <TableCell>
                                                        <Typography variant="h9">
                                                {props.row.for.phone ? props.row.for.phone : "Nije navedeno"}
                                                        </Typography>
                                                    </TableCell>
                                                    <TableCell>
                                                        <Typography variant="h9">
                                                            <b>Email:</b>
                                                        </Typography>
                                                    </TableCell>
                                                    <TableCell>
                                                    <Typography variant="h9">
                                                {props.row.for.email ? props.row.for.email : "Nije navedeno"}

                                                        </Typography>
                                           </TableCell>                                           
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

export default Row;
