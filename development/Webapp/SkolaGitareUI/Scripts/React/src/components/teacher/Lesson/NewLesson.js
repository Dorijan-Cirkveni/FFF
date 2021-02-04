import React from "react";
import Button from "@material-ui/core/Button";
import LessonForm from "./LessonForm";
import Grid from "@material-ui/core/Grid";
import { Paper } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import pink from "@material-ui/core/colors/pink";
import request from "../../../request.js";


const useStyles = makeStyles((theme) => ({
    paper: {
        padding: theme.spacing(2),
        textAlign: "center",
    },
    root: {
        width: "50%",
        margin: "2%",
        padding: theme.spacing(1)
    },
    papperTabs: {},
    button: {
        backgroundColor: pink[500],
        marginTop: "5px",
        color: "white"
    },
    contentGrid: {},
}));

export default function NewLesson(props) {
    const classes = useStyles();

    const [data, setData] = React.useState({
        name: "",
        notes: "",
        students: [],
        file: {},
    });

    const resetData = () => {
        setData({
            name: "",
            notes: "",
            students: [],
            file: {},
        });
    }
    const [students, setStudents] = React.useState([]);
    React.useEffect(() => {
        let mounted = true;
        request(props.accessToken, {
            method: "get",
            url: 'Members/Students',
        }).then((data) => {
            if (mounted) {
                setStudents(data);
            }
        });
        return () => (mounted = false);
    }, []);

    const handleChange = (field, values) => {
        setData({ ...data, [field]: values });
    };
    const handleSave = () => {
        let temp = data;
        resetData()
        temp.students.forEach((student) => {
            let body = {
                for: student.id,
                name: temp.name,
                notes: temp.notes
            }
            request(props.accessToken, {
                method: "post",
                url: 'Lessons',
                data: body 
            }).then((data) => {
            });
        });
       
    };
    return (
        <Grid container spacing={2} justify="center" alignItems="center">

            <Paper className={classes.root}>
                <Grid container spacing={2} justify="center" alignItems="center">
                    <Grid item xs={6} className={classes.contentGrid} align="center">

                    <LessonForm
                            lesson={data}
                            handleChange={handleChange}
                            isNew={true}
                            title={"Nova lekcija"}
                            students={students}
                    ></LessonForm>

                    <Button color="primary" onClick={handleSave}>
                        Save
                    </Button>
                    </Grid>
                </Grid>

            </Paper>
        </Grid>

    );
}
