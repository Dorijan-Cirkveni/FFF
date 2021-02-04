import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Typography from "@material-ui/core/Typography";
import Paper from "@material-ui/core/Paper";
import Toolbar from "@material-ui/core/Toolbar";
import Input from "@material-ui/core/Input";
import InputAdornment from "@material-ui/core/InputAdornment";
import SearchIcon from "@material-ui/icons/Search";
import request from "../../../request.js";
import Row from "./UserRow";




const useStyles = makeStyles({
    title: {
        flex: "1 1 100%",
    },
    root: {
        width: "100%",
    },
    container: {
        maxHeight: 400,
    },
});



export default function UsersTable(props) {
    let classes = useStyles();
    const [filter, setFilter] = React.useState("");
    const [users, setUsers] = React.useState(null);

    const handleFilterChange = (event) => {
        setFilter(event.target.value);
    };

    React.useEffect(() => {
        let mounted = true;
        request(props.accessToken, {
            method: "get",
            url: props.url,
        }).then((data) => {
            if (mounted) {
                setUsers(data);
            }
        });
        return () => (mounted = false);
    }, []);

    function handleChange(succ) {

        request(props.accessToken, {
            method: "get",
            url: props.url,
        }).then((data) => {

            setLessons(data);




        })
    }
    return (

      <Paper className={classes.root}>
        <TableContainer className={classes.container}>
            {users ?
                (<div>
                {props.header && <Toolbar>
                    <Typography
                        className={classes.title}
                        variant="h6"
                        id={`tableHeader-${props.titleId}`}
                        component="div"
                    >
                        {props.header}
                    </Typography>
                </Toolbar>}
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Ime</TableCell>
                            <TableCell align="left">Prezime</TableCell>
                            <TableCell>Kontakt</TableCell>
                            <TableCell align="right">
                                <Input
                                    id="input-with-icon-adornment"
                                    variant="outlined"
                                    placeholder="Ime"
                                    value={filter}
                                    onChange={handleFilterChange}
                                    startAdornment={
                                        <InputAdornment position="start">
                                            <SearchIcon />
                                        </InputAdornment>
                                    }
                                />
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {users
                            .filter(
                                (user) =>
                                    (filter === "" ||
                                        user.name.toUpperCase().includes(
                                            filter.toUpperCase()
                                        ) ||
                                        user.surname.toUpperCase().includes(
                                            filter.toUpperCase()
                                        )
                                    ))
                            .map((user) => {
                                return <Row key={user.id} row={user} accessToken={props.accessToken} user={user} />;
                            })}
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
                        Nema dostupnih korisnika
                            </Typography>
                </Toolbar>
            </div>}
            </TableContainer>
        </Paper >
    );
}
