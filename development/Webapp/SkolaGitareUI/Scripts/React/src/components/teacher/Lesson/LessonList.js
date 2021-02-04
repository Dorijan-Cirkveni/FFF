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
import Row from "./LessonRow";


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

export default function LessonList(props) {
    let classes = useStyles();
    const [filter, setFilter] = React.useState("");
    const [lessons, setLessons] = React.useState(null);
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
                setLessons(data);
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




        })}
   
    
    return (
        <Paper className={classes.root}>
            <TableContainer className={classes.container}>
                {lessons ?
                    (<div>
                <Toolbar>
                    <Typography
                        stickyHeader
                        variant="h6"
                        id="tableTitle"
                        component="div"
                        className={classes.title}
                    >
                        {props.title}
                    </Typography>
                </Toolbar>
                
                    <Table stickyHeader aria-label="sticky table">
                        <TableHead>
                            <TableRow>
                                <TableCell>Ime</TableCell>
                                <TableCell>Detalji</TableCell>
                                <TableCell align="right">
                                    <Input
                                        id="input-with-icon-adornment"
                                        variant="outlined"
                                        placeholder="Ime studenta"
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
                            {lessons
                                .filter(
                                    (lesson) =>
                                        filter === "" ||
                                        lesson.for.name
                                            .toUpperCase()
                                            .includes(filter.toUpperCase()) ||
                                        lesson.for.surname
                                            .toUpperCase()
                                            .includes(filter.toUpperCase())
                                )
                                    .map((lesson) => {


                                    return <Row key={lesson.name} row={lesson} accessToken={props.accessToken} handleEdit={handleChange} handleDelete={handleChange}/>;
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
                                Nema dostupnih lekcija
                            </Typography>
                        </Toolbar>
                     </div>}
            </TableContainer>
        </Paper>
    );
}
