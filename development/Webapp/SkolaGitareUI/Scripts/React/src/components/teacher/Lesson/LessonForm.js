import React from "react";
import { makeStyles } from "@material-ui/core/styles";


import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";
import Grid from "@material-ui/core/Grid";
import PublishIcon from "@material-ui/icons/Publish";

import pink from "@material-ui/core/colors/pink";



const useStyles = makeStyles((theme) => ({
    root: {
        width: "100%",
    },
    inputField: {
        "&$disabled": {
            opacity: "90%",
        },
        margin: theme.spacing(1),
        width: "105%",
        paddingRight: "10%",
    },

    notesField: {
        paddingRight: "5%",

        margin: theme.spacing(1),
    },
    selectStudents: {
        margin: theme.spacing(1),
        width: "100%",
        paddingRight: "5%",
    },
    input: {
        display: "none",
    },
    title: {
        color: pink[500]
    }
}));

export default function LessonForm(props) {
    let classes = useStyles();
    return (
        <form className={classes.root} noValidate autoComplete="off">
            <Grid container spacing={1}>
                <Grid item xs={12}>
                    <Grid container>
                        
                        <Grid item xs={12}>

                        <h4 className={classes.title}>{props.title}</h4>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                className={classes.inputField}
                                label="Ime:"
                                value={props.lesson.name}
                                onChange={(e) => props.handleChange("name", e.target.value)}
                                id="lesson-name-field"
                                size="small"
                            />
                        </Grid>
                        
                        <Grid item xs={12} style={{ display: props.isNew }}>
                            {props.isNew && (
                                <Autocomplete
                                    multiple
                                    id="student-lesson-picker"
                                    size="small"
                                    options={props.students}
                                    getOptionLabel={(student) =>
                                       student? student.name + " " + student.surname : ""
                                    }
                                    filterSelectedOptions
                                    renderInput={(params) => (
                                        <TextField
                                            {...params}
                                            size="small"
                                            className={classes.selectStudents}
                                            label="Učenici"
                                        />
                                    )}
                                    value={props.lesson.students}
                                    onChange={(event, values) =>
                                        props.handleChange("students", values)
                                    }
                                />
                            )}
                        </Grid>
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        className={classes.notesField}
                        label="Notes"
                        id="outlined-size-small"
                        value={props.lesson.notes}
                        onChange={(e) => props.handleChange("notes", e.target.value)}
                        size="small"
                        fullWidth
                        multiline
                        inputProps={{ maxLength: 500 }}

                    />
                </Grid>
                <Grid item xs={12}>
                    <label htmlFor="button-file">

                    <input
                        style={{ display: 'none' }}

                        id="button-file"
                        type="file"
                        onChange={(e) => props.handleChange("file", e.target.files[0])}
                    />
                        <Button
                            variant="contained"
                            size="small"
                            startIcon={<PublishIcon />}
                            color="secondary"
                            component="span"
                        >
                            Dodaj materijale
                    </Button>
                        </label>
                </Grid>
            </Grid>
        </form>
    );
}
